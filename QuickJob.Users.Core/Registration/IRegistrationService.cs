using QuickJob.Users.DataModel.Api.Requests.Registration;
using QuickJob.Users.DataModel.Api.Responses.Auth;
using QuickJob.Users.DataModel.Api.Responses.Registration;

namespace Users.Service.Registration;

public interface IRegistrationService
{
    Task<InitCreateUserResponse> InitCreateUser(InitCreateUserRequest request);
    Task<LoginResponse> ConfirmCreateUser(ConfirmCreateUserRequest request);
}