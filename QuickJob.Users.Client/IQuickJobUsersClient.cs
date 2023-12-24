using QuickJob.Users.Client.Clients;

namespace QuickJob.Users.Client;

public interface IQuickJobUsersClient
{
    //IAuthClient Auth { get; }

    //IRegistrationClient Registration { get; }

    IUsersClient Users { get; }
}
