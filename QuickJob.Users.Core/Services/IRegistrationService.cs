using QuickJob.Users.DataModel.Api.Requests.Registration;

namespace Users.Service.Services;

public interface IRegistrationService
{
    Task InitCreateUser(InitCreateUserRequest initCreateUserRequest);
    Task<object?> ConfirmCreteUser(string email, string password);
}