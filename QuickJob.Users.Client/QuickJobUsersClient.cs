using System.Net.Http;
using QuickJob.Users.Client.Clients;

namespace QuickJob.Users.Client;

public class QuickJobUsersClient : IQuickJobUsersClient
{
    public QuickJobUsersClient(HttpClient httpClient, string apiUrl)
    {
        var requestSender = new StandaloneRequestSender(httpClient);
        Users = new UsersClient(requestSender, apiUrl);
    }

    public IUsersClient Users { get; }
}
