namespace QuickJob.Users.DataModel.Api.Responses.Auth;

public sealed record LoginResult(Tuple<AuthResponseBase, string> authResponse)
{
    public string RefreshToken { get; set; } = authResponse.Item2;
    public AuthResponseBase AuthResponseBase { get; set; } = authResponse.Item1;
}