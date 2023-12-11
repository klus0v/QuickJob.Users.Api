using QuickJob.Users.DataModel.Api.Responses.Auth;

namespace Users.Service.Services;

public interface IAuthService
{
    Task<LoginResult> Login(string email, string password);
    Task<AuthResponseBase> RefreshToken(string token);
}