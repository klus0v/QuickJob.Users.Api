namespace QuickJob.Users.DataModel.Configuration;

public sealed record ServiceSettings
{
    public List<string> Origins { get; set; }
    public string FrontRegisterUrl { get; set; }
}