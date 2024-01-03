using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickJob.Users.DataModel.Api.Requests.Registration;
using Users.Service.Registration;
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
    public async Task<IActionResult> InitCreteUser(InitCreateUserRequest initCreateUserRequest)
    {
        var response = await registrationService.InitCreateUser(initCreateUserRequest);
        return Ok(response);
    }
    
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmCreteUser(ConfirmCreateUserRequest request)
    {
        var loginResult = await registrationService.ConfirmCreateUser(request);
        return Ok(loginResult);
    }
}
