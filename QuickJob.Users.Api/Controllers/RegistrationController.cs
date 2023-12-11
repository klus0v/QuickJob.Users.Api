using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Service.Services;

namespace Users.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationService registrationService; 
    
    public RegistrationController(IRegistrationService registrationService)
    {
        this.registrationService = registrationService;
    }
    
    [HttpPost("request")]
    public async Task<IActionResult> InitCreteUser([FromForm, Required, EmailAddress] string email, [FromForm, Required] string password)
    {
        var loginResult = await registrationService.InitCreteUser(email, password);
        return Ok(loginResult);
    }
    
    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmCreteUser([FromForm, Required, EmailAddress] string email, [FromForm, Required] string password)
    {
        var loginResult = await registrationService.ConfirmCreteUser(email, password);
        return Ok(loginResult);
    }
}
