using System.Net.Http.Headers;
using FS.Keycloak.RestApiClient.Api;
using Newtonsoft.Json.Linq;
using QuickJob.Users.DataModel.Api.Responses.Auth;
using QuickJob.Users.DataModel.Api.Responses.Users;
using QuickJob.Users.DataModel.Configuration;
using QuickJob.Users.DataModel.Exceptions;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;

namespace Users.Service.Services.Implementations;

public sealed class UsersService : IUsersService
{
    private readonly ILog log;
    private readonly HttpClient httpClient;
    private readonly IUserApi userApi;
    private readonly KeycloackSettings keycloackSettings;

    public UsersService(ILog log, IConfigurationProvider configurationProvider, IUserApi userApi)
    {
        this.log = log;
        this.userApi = userApi;
        httpClient = new HttpClient();
        keycloackSettings = configurationProvider.Get<KeycloackSettings>();
    }

    public async Task<object?> DeleteUser(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponse> GetUser(string userId)
    {
        return new UserResponse(Guid.Parse(userId));
    }

    public async Task<object?> SearchUsers(string email, string password)
    {
        throw new NotImplementedException();
    }
}