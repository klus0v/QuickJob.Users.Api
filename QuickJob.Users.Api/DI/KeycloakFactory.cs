using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Authentication.Client;
using FS.Keycloak.RestApiClient.Client;
using FS.Keycloak.RestApiClient.Model;
using QuickJob.Users.DataModel.Configuration;
using ApiClientFactory = FS.Keycloak.RestApiClient.ClientFactory.ApiClientFactory;
using IConfigurationProvider = Vostok.Configuration.Abstractions.IConfigurationProvider;

namespace Users.Api.DI;

internal class KeycloakFactory
{
    private readonly KeycloackSettings keycloackSettings;
    private readonly AuthenticationHttpClient authenticationHttpClient;

    public KeycloakFactory(IConfigurationProvider configuration)
    {
        keycloackSettings = configuration.Get<KeycloackSettings>();
        authenticationHttpClient = CreateHttpClient();
    }

    public AuthenticationHttpClient GetHttpClient() => 
        authenticationHttpClient;

    public IUsersApi GetUsersClient() => 
        ApiClientFactory.Create<UsersApi>(authenticationHttpClient);

    public IUserApi GetUserClient() => 
        ApiClientFactory.Create<UserApi>(authenticationHttpClient);

    //todo
    private AuthenticationHttpClient CreateHttpClient() => 
        new KeycloakHttpClient(keycloackSettings.BaseUrl, keycloackSettings.Login, keycloackSettings.Password);
}