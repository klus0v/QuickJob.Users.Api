using FS.Keycloak.RestApiClient.Model;
using Microsoft.AspNetCore.Mvc;
using Users.Service.Services;

namespace QuickJob.Users.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService usersService; 
    
    public UsersController(IUsersService usersService)
    {
        this.usersService = usersService;
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await usersService.GetUser(userId.ToString());
        return Ok(user);
    }

    [HttpPatch("{userId}")]
    public async Task<IActionResult> PatchUser(Guid userId, [FromBody] UserRepresentation representation)
    {
        var user = await usersService.PatchUser(userId.ToString(), representation);
        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await usersService.DeleteUser(userId.ToString());
        return Ok();
    }
}
