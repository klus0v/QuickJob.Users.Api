namespace Users.Service.Services;

public interface IRegistrationService
{
    Task<object?> InitCreteUser(string email, string password);
    Task<object?> ConfirmCreteUser(string email, string password);
}