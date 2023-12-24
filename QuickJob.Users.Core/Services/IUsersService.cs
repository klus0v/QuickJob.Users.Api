using QuickJob.Users.DataModel.Api.Responses.Users;

namespace Users.Service.Services;

public interface IUsersService
{
    Task<object?> DeleteUser(string userId);
    Task<UserResponse> GetUser(string userId);
    Task<object?> SearchUsers(string email, string password);
}