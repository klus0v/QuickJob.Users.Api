namespace Users.Service.Services;

public interface IUsersService
{
    Task<object?> DeleteUser(string userId);
    Task<object?> GetUser(string userId);
    Task<object?> SearchUsers(string email, string password);
}