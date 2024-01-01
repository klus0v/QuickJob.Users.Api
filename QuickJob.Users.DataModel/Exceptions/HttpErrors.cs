namespace QuickJob.Users.DataModel.Exceptions;

public static class HttpErrors
{
    private const string KeycloackError = "Keycloack";
    private const string NotFoundError = "NotFound";
    private const string UnauthorizedApiError = "UnauthorizedApi";
    private const string NoAccessError = "NoAccess";
    private const string NoAccessToApiError = "NoAccessToApi";

    public static CustomHttpError Keycloack(string error) => new(KeycloackError, $"Keycloack error: {error}");
    public static CustomHttpError UnauthorizedApi => new(UnauthorizedApiError, "Api is unauthorized");
    public static CustomHttpError NotFound(object itemKey) => new(NotFoundError, $"Not found item with key: '{itemKey}'");
    public static CustomHttpError NoAccess(object itemKey) => new(NoAccessError, $"No access to item with key: '{itemKey}'");
    public static CustomHttpError NoAccessToApi => new(NoAccessToApiError, "No access to api'");

}
