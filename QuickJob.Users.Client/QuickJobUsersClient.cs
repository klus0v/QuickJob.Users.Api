using System;
using System.Net.Http;
using System.Net.Http.Headers;
using QuickJob.Users.Client.Clients;

namespace QuickJob.Users.Client;

public class QuickJobUsersClient : IQuickJobUsersClient
{
    public QuickJobUsersClient(string baseUrl, string apiKey)
    {
        var httpClient = new HttpClient { BaseAddress = new Uri(baseUrl)};
        httpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("api.key", apiKey);
        var requestSender = new StandaloneRequestSender(httpClient);
        
        Users = new UsersClient(requestSender);
    }

    public IUsersClient Users { get; }
}
