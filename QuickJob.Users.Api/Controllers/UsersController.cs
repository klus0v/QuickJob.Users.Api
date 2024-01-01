using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
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
    
    [AllowAnonymous]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var result = await usersService.GetUser(userId.ToString());
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers([FromForm, Required, EmailAddress] string email, [FromForm, Required] string password)
    {
        var loginResult = await usersService.SearchUsers(email, password);
        return Ok(loginResult);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromForm, Required] string userId)
    {
        return Ok(await usersService.DeleteUser(userId));
    }
}
