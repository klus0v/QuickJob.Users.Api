using FS.Keycloak.RestApiClient.Client;
using QuickJob.Users.DataModel.Configuration;
using IConfigurationProvider = Vostok.Configuration.Abstractions.IConfigurationProvider;

namespace Users.Api.DI;

internal class KeycloakFactory
{
    private readonly KeycloackSettings keycloackSettings;

    public KeycloakFactory(IConfigurationProvider configuration) => 
        keycloackSettings = configuration.Get<KeycloackSettings>();

    public KeycloakHttpClient GetClient()
    {
        var httpClient = new KeycloakHttpClient(keycloackSettings.BaseUrl, keycloackSettings.Login, keycloackSettings.Password);
        return httpClient;
    }
}
