using System.Net;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Client;
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
    private readonly IUsersApi usersApi;
    private readonly KeycloackSettings keycloackSettings;

    public UsersService(ILog log, IConfigurationProvider configurationProvider, IUserApi userApi, IUsersApi usersApi)
    {
        this.log = log;
        this.userApi = userApi;
        this.usersApi = usersApi;
        keycloackSettings = configurationProvider.Get<KeycloackSettings>();
    }

    public async Task DeleteUser(string userId)
    {
        log.Info($"Delete user: {userId}");
        await GetUserInternal(userId);
        await userApi.DeleteUsersByIdAsync(keycloackSettings.RealmName, userId);
    }
    
    public async Task<UserRepresentation> CreateUser(UserRepresentation user)
    {
        var result = await usersApi.GetUsersAsync(keycloackSettings.RealmName, username: user.Email);
        if (result.Count > 0)
            throw new CustomHttpException(HttpStatusCode.Conflict, HttpErrors.UserAlreadyCreated(user.Email));
        await usersApi.PostUsersAsync(keycloackSettings.RealmName, user);
        log.Info($"Create user: {user.Id}");
        var userResult = await usersApi.GetUsersAsync(keycloackSettings.RealmName, username: user.Email);
        return userResult.First();
    }

    public async Task<UserRepresentation> PatchUser(string userId, UserRepresentation representation)
    {
        log.Info($"Patch user: {userId}");
        await GetUserInternal(userId);
        await userApi.PutUsersByIdAsync(keycloackSettings.RealmName, userId, representation);
        return await GetUserInternal(userId);
    }

    public async Task<UserRepresentation> GetUser(string userId) => 
        await GetUserInternal(userId);

    private async Task<UserRepresentation> GetUserInternal(string userId)
    {
        try
        {
            var userResult = await userApi.GetUsersByIdAsync(keycloackSettings.RealmName, userId);
            return userResult;
        }
        catch (Exception e)
        {
            var error = (ApiException)e;
            if (error.ErrorCode == 404)
                throw new CustomHttpException(HttpStatusCode.NotFound, HttpErrors.NotFound(userId));

            throw new CustomHttpException(error.ErrorCode, HttpErrors.Keycloack(error.Message));
        }
    }
}