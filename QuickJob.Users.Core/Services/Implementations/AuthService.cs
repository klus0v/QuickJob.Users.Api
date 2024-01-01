using System.Net;
using System.Net.Http.Headers;
using FS.Keycloak.RestApiClient.Api;
using Newtonsoft.Json.Linq;
using QuickJob.Users.DataModel.Api.Responses.Auth;
using QuickJob.Users.DataModel.Configuration;
using QuickJob.Users.DataModel.Exceptions;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;

namespace Users.Service.Services.Implementations;

public sealed class AuthService : IAuthService
{
    private readonly ILog log;
    private readonly HttpClient httpClient;
    private readonly IUserApi userApi;
    private readonly KeycloackSettings keycloackSettings;

    public AuthService(ILog log, IConfigurationProvider configurationProvider, IUserApi userApi)
    {
        this.log = log;
        this.userApi = userApi;
        httpClient = new HttpClient();
        keycloackSettings = configurationProvider.Get<KeycloackSettings>();
    }

    public async Task<LoginResponse> Login(string email, string password) => 
        new(await AuthUser(email, password));

    public async Task<AuthResponseBase> RefreshToken(string token) => 
        (await AuthUser(token: token)).Item1;

    public async Task Logout(string userId) => 
        await userApi.PostUsersLogoutByIdAsync(keycloackSettings.RealmName, userId);

    private async Task<Tuple<AuthResponseBase, string>> AuthUser(string? username = null, string? password = null, string? token = null)
    {
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var parameters = new Dictionary<string, string> { {"client_id", keycloackSettings.ClientId}, {"client_secret", keycloackSettings.ClientSecret} };
        
        if (username != null && password != null)
        {
            parameters.Add("username", username);
            parameters.Add("password", password);
            parameters.Add("grant_type", "password");
        }
        if (token != null)
        {
            parameters.Add("refresh_token", token);
            parameters.Add("grant_type", "refresh_token");
        }
        
        var content = new FormUrlEncodedContent(parameters);

        log.Info($"Send login request for: '{username}'");
        var response = await httpClient.PostAsync(keycloackSettings.AuthUrl, content);
        var result = await response.Content.ReadAsStringAsync();
        var jsonResult = JObject.Parse(result);
        
        if (!response.IsSuccessStatusCode)
        {
            log.Warn($" Login error for user: '{username}'");
            throw new CustomHttpException(HttpStatusCode.Unauthorized, HttpErrors.Keycloack(jsonResult["error_description"]?.ToString() ?? ""));
        }

        log.Info($"User with email: '{username ?? "TOKEN"}' login");
        return new Tuple<AuthResponseBase, string>(new AuthResponseBase
        {
            AccessToken = jsonResult["access_token"]?.ToString() ?? "",
            ExpiresIn = (int)(jsonResult["expires_in"] ?? 0),
            RefreshExpiresIn = (int)(jsonResult["refresh_expires_in"] ?? 0),
            SessionState = jsonResult["session_state"]?.ToString() ?? ""
        }, jsonResult["refresh_token"]?.ToString() ?? "");

    }
}