using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Users.DataModel.Api.Requests.Registration;
using QuickJob.Users.DataModel.Api.Responses.Auth;
using QuickJob.Users.DataModel.Api.Responses.Registration;
using Users.Service.Registration;

namespace QuickJob.Users.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService registrationService; 
    private const string RefreshTokenKey = "QuickJob_RefreshToken";
    
    public RegistrationController(IRegistrationService registrationService)
    {
        this.registrationService = registrationService;
    }
    
    [HttpPost("request")]
    [ProducesResponseType(typeof(InitCreateUserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> InitCreteUser(InitCreateUserRequest initCreateUserRequest)
    {
        var response = await registrationService.InitCreateUser(initCreateUserRequest);
        return Ok(response);
    }
    
    [HttpPost("confirm")]
    [ProducesResponseType(typeof(AuthResponseBase), StatusCodes.Status200OK)]
    public async Task<IActionResult> ConfirmCreteUser(ConfirmCreateUserRequest request)
    {
        var loginResult = await registrationService.ConfirmCreateUser(request);
        SetCookie(RefreshTokenKey, loginResult.RefreshToken);
        return Ok(loginResult.AuthResponseBase);
    }
    
    private void SetCookie(string key, string value, bool httpOnly = true)
    {
        var options = new CookieOptions
        {
            HttpOnly = httpOnly,
            Path = "auth/login"
        };
        HttpContext.Response.Cookies.Append(key, value, options);
    }
}
