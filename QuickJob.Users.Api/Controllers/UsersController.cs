using System.Net;
using FS.Keycloak.RestApiClient.Model;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Users.DataModel.Configuration;
using QuickJob.Users.DataModel.Exceptions;
using Users.Service.Services;
using IConfigurationProvider = Vostok.Configuration.Abstractions.IConfigurationProvider;

namespace QuickJob.Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService usersService;
    private readonly HashSet<string> AllowedApiKeys;

    public UsersController(IUsersService usersService, IConfigurationProvider configurationProvider)
    {
        this.usersService = usersService;
        AllowedApiKeys = configurationProvider.Get<ServiceSettings>().AllowedApiKeys;
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        CheckAccess(HttpContext);
        var user = await usersService.GetUser(userId.ToString());
        return Ok(user);
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> PatchUser(Guid userId, [FromBody] UserRepresentation representation)
    {
        CheckAccess(HttpContext);
        var user = await usersService.PatchUser(userId.ToString(), representation);
        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        CheckAccess(HttpContext);
        await usersService.DeleteUser(userId.ToString());
        return Ok();
    }
    
    private void CheckAccess(HttpContext context)
    {
        var value = context.Request.Headers.Authorization.FirstOrDefault();
        var apiKey = value?.Replace("api.key ", string.Empty);
        
        if (apiKey is null)
            throw new CustomHttpException(HttpStatusCode.Unauthorized, HttpErrors.UnauthorizedApi);
        
        var hasAccess = AllowedApiKeys.Contains(apiKey);
        if (!hasAccess)
            throw new CustomHttpException(HttpStatusCode.Forbidden, HttpErrors.NoAccessToApi);
    }
}
