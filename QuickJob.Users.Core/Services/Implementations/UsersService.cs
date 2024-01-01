using System.Net;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Model;
using QuickJob.Users.DataModel.Configuration;
using QuickJob.Users.DataModel.Exceptions;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;

namespace Users.Service.Services.Implementations;

public sealed class UsersService : IUsersService
{
    private readonly ILog log;
    private readonly IUserApi userApi;
    private readonly KeycloackSettings keycloackSettings;

    public UsersService(ILog log, IConfigurationProvider configurationProvider, IUserApi userApi)
    {
        this.log = log;
        this.userApi = userApi;
        keycloackSettings = configurationProvider.Get<KeycloackSettings>();
    }

    public async Task DeleteUser(string userId)
    {
        log.Info($"Delete user: {userId}");

        await GetUser(userId);
        await userApi.DeleteUsersByIdAsync(keycloackSettings.RealmName, userId);
    }

    public async Task<UserRepresentation> GetUser(string userId)
    {
        var userResponse = await userApi.GetUsersByIdAsync(keycloackSettings.RealmName, userId);
        if (userResponse != null) 
            return userResponse;
        log.Warn($"User: {userId} not found");
        throw new CustomHttpException(HttpStatusCode.NotFound, HttpErrors.NotFound(userId));
    }

    public async Task<UserRepresentation> PatchUser(string userId, UserRepresentation representation)
    {
        log.Info($"Patch user: {userId}");
        await GetUser(userId);
        await userApi.PutUsersByIdAsync(keycloackSettings.RealmName, userId, representation);
        return await GetUser(userId);
    }
}