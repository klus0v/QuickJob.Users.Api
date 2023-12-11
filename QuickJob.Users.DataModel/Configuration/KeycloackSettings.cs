﻿namespace QuickJob.Users.DataModel.Configuration;

public sealed record KeycloackSettings
{
    public string BaseUrl { get; set; }
    public string RealmName { get; set; }

    public string SubClaim { get; set; }
    public string AuthUrl { get; set; }
    public string Authority { get; set; }
    public string Audience { get; set; }

    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}