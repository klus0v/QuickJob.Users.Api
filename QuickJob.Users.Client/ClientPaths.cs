using System;

namespace QuickJob.Users.Client;

internal static class ClientPaths
{
    public const string Users = "users";

    private const string Delimiter = "/";

    public static string User(Guid userId) => Users + Delimiter + userId;
    
}