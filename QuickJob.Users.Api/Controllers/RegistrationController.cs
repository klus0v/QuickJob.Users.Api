using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Users.DataModel.Api.Requests.Registration;
using Users.Service.Services;

namespace QuickJob.Users.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService registrationService; 
    private readonly IAuthService authService; 
    
    public RegistrationController(IRegistrationService registrationService, IAuthService authService)
    {
        this.registrationService = registrationService;
        this.authService = authService;
    }
    
    [HttpPost("request")]
    public async Task<IActionResult> InitCreteUser([FromBody] InitCreateUserRequest initCreateUserRequest)
    {
        await registrationService.InitCreateUser(initCreateUserRequest);
        await authService.Login(initCreateUserRequest.Email, initCreateUserRequest.Password);
        return Created("registration", null);
    }
    //todo add email verification 
    // [HttpPost("confirm")]
    // public async Task<IActionResult> ConfirmCreteUser([FromForm, Required, EmailAddress] string email, [FromForm, Required] string password)
    // {
    //     var loginResult = await registrationService.ConfirmCreteUser(email, password);
    //     return Ok(loginResult);
    // }
}
