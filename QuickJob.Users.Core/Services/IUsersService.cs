using FS.Keycloak.RestApiClient.Model;

namespace Users.Service.Services;

public interface IUsersService
{
    Task DeleteUser(string userId);
    Task<UserRepresentation> GetUser(string userId);
    Task<UserRepresentation> PatchUser(string userId, UserRepresentation representation);
}