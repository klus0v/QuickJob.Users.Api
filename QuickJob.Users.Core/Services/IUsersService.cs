using FS.Keycloak.RestApiClient.Model;

namespace Users.Service.Services;

public interface IUsersService
{
    Task<object?> DeleteUser(string userId);
    Task<UserRepresentation> GetUser(string userId);
    Task<object?> SearchUsers(string email, string password);
}