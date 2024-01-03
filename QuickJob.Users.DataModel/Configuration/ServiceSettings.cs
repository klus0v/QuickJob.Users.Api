namespace QuickJob.Users.DataModel.Configuration;

public sealed record ServiceSettings
{
    public List<string> Origins { get; set; }
    public bool SendEmailConfirmation { get; set; }
    public HashSet<string> AllowedApiKeys { get; set; }
    public string NotificationsApiKey { get; set; }
    public string NotificationsApiBaseUrl { get; set; }
}