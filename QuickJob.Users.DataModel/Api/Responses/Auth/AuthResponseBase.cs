namespace QuickJob.Users.DataModel.Api.Responses.Auth;

public record AuthResponseBase
{
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public int RefreshExpiresIn { get; set; }
    public string SessionState { get; set; }

}