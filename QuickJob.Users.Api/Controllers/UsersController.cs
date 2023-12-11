using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Service.Services;

namespace Users.Api.Controllers;

[Authorize]
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
    public async Task<IActionResult> GetUser([FromForm, Required] string userId)
    {
        return Ok(await usersService.GetUser(userId));
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
