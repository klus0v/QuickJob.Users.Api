using QuickJob.Users.DataModel.Api.Responses.Auth;

namespace Users.Service.Services;

public interface IAuthService
{
    Task<LoginResponse> Login(string email, string password);
    Task<AuthResponseBase> RefreshToken(string token);
    Task Logout(string userId);
}