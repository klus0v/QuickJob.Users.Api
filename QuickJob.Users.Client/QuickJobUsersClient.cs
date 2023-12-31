using System.Net.Http;
using QuickJob.Users.Client.Clients;

namespace QuickJob.Users.Client;

public class QuickJobUsersClient : IQuickJobUsersClient
{
    public QuickJobUsersClient(HttpClient httpClient)
    {
        var requestSender = new StandaloneRequestSender(httpClient);
        Users = new UsersClient(requestSender);
    }

    public IUsersClient Users { get; }
}
