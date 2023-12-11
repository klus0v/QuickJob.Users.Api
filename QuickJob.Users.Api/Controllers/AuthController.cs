using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Service.Services;

namespace Users.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private const string RefreshTokenKey = "QuickJob_RefreshToken";
    
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm, Required, EmailAddress] string email, [FromForm, Required] string password)
    {
        var loginResult = await authService.Login(email, password);
        SetCookie(RefreshTokenKey, loginResult.RefreshToken);
        
        return Ok(loginResult.AuthResponseBase);
    }

    [HttpPost("token/refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        if (!TryGetCookie(RefreshTokenKey, out var token))
            return Unauthorized("Token is not present");
        var refreshResult = await authService.RefreshToken(token);
        return Ok(refreshResult);
    }

    [Authorize]
    [HttpPost("logout")]
    public Task<IActionResult> Logout()
    {
        throw new NotImplementedException();
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

    private bool TryGetCookie(string key, out string cookie )
    {
        if (HttpContext.Request.Cookies.TryGetValue(key, out var value))
        {
            cookie = value;
            return true;
        }
        cookie = string.Empty;
        return false;
    }
}
