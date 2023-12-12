using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Model;
using QuickJob.Users.DataModel.Api.Requests.Registration;
using QuickJob.Users.DataModel.Api.Responses.Registration;
using QuickJob.Users.DataModel.Configuration;
using QuickJob.Users.DataModel.Constants;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;

namespace Users.Service.Services.Implementations;

public sealed class RegistrationService : IRegistrationService
{
    private readonly ILog log;
    private readonly IUsersApi usersApi;
    private readonly KeycloackSettings keycloackSettings;

    public RegistrationService(ILog log, IUsersApi usersApi, IConfigurationProvider configurationProvider)
    {
        this.log = log;
        this.usersApi = usersApi;
        keycloackSettings = configurationProvider.Get<KeycloackSettings>();
    }

    public async Task InitCreateUser(InitCreateUserRequest initCreateUserRequest)
    {
        var user = new UserRepresentation
        {
            Email = initCreateUserRequest.Email,
            Username = initCreateUserRequest.Email,
            EmailVerified = true,
            Enabled = true,
            Attributes = new Dictionary<string, List<string>>
            {
                { KeycloackUserAttributes.Phone, new List<string> { initCreateUserRequest.Phone } },
                { KeycloackUserAttributes.Fio, new List<string> { initCreateUserRequest.Fio } }
            },
            Credentials = new List<CredentialRepresentation> { new() { Type = KeycloackUserAttributes.Password, Value = initCreateUserRequest.Password}}
        };

        await usersApi.PostUsersAsync(keycloackSettings.RealmName, user);
    }

    public async Task<object?> ConfirmCreteUser(string email, string password)
    {
        throw new NotImplementedException();
    }
}