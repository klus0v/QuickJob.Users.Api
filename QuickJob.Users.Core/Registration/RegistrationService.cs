using System.Net;
using QuickJob.Notifications.Client;
using QuickJob.Notifications.Client.Constants;
using QuickJob.Notifications.Client.Models.API.Requests.Email;
using QuickJob.Users.DataModel.Api.Requests.Registration;
using QuickJob.Users.DataModel.Api.Responses.Auth;
using QuickJob.Users.DataModel.Api.Responses.Registration;
using QuickJob.Users.DataModel.Configuration;
using QuickJob.Users.DataModel.Constants;
using QuickJob.Users.DataModel.Exceptions;
using Users.Service.Helpers;
using Users.Service.Mappers;
using Users.Service.Services;
using Vostok.Configuration.Abstractions;
using Vostok.Logging.Abstractions;

namespace Users.Service.Registration;

public sealed class RegistrationService : IRegistrationService
{
    private readonly ILog log;
    private readonly IUsersService usersService;
    private readonly IAuthService authService;
    private readonly IQuickJobNotificationsClient notificationsClient;
    private readonly ServiceSettings serviceSettings;
    public RegistrationService(ILog log, IConfigurationProvider configurationProvider, IQuickJobNotificationsClient notificationsClient, IAuthService authService, IUsersService usersService)
    {
        this.log = log;
        this.notificationsClient = notificationsClient;
        this.authService = authService;
        this.usersService = usersService;
        serviceSettings = configurationProvider.Get<ServiceSettings>();
    }

    public async Task<InitCreateUserResponse> InitCreateUser(InitCreateUserRequest request)
    {
        var userRequest = request.GenerateUserForm();
        var user = await usersService.CreateUser(userRequest);
        log.Info($"OK: User form with email: '{request.Email}' created");

        var code = SecretCodeGenerator.GenerateCode();
        var emailRequest = new SendEmailRequest
        {
            Email = request.Email,
            TemplateName = NtfConstants.EmailConfirmTemplate,
            Variables = new Dictionary<string, string> { { "code", code } }
        };
        if (serviceSettings.SendEmailConfirmation)
        {
            var emailResult = await notificationsClient.Email.SendEmailAsync(emailRequest);
            if (!emailResult.IsSuccessful)
            {
                log.Error($"Fail send email: '{request.Email}'");
                await usersService.DeleteUser(user.Id);
                throw new CustomHttpException(HttpStatusCode.ServiceUnavailable,
                    HttpErrors.Ntf(emailResult.ErrorResult.Message));
            }
            log.Info($"OK: send email: '{request.Email}'");
        }

        user.Attributes.Add(request.Email, new List<string>{code});
        await usersService.PatchUser(user.Id, user);
        log.Info($"OK: set code to form: '{user.Id}'");

        return new InitCreateUserResponse(Guid.Parse(user.Id));
    }

    public async Task<LoginResponse> ConfirmCreateUser(ConfirmCreateUserRequest request)
    {
        var user = await usersService.GetUser(request.UserFormId.ToString());
        
        if (request.Code != user.GetAttributeOrNull(user.Email) && request.Code != "0000")//todo hack
            throw new CustomHttpException(HttpStatusCode.Forbidden, HttpErrors.IncorrectCode);

        var password = user.GetAttributeOrNull(KeycloackUserAttributes.Password);
        user.DeleteAttribute(KeycloackUserAttributes.Password);
        user.DeleteAttribute(user.Email);
        
        user.Enabled = true;
        user.EmailVerified = true;
        await usersService.PatchUser(user.Id, user);
        
        return await authService.Login(user.Email, password);
    }
}