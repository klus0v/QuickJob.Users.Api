using System;
using System.Threading.Tasks;
using QuickJob.Users.Client.Models;
using QuickJob.Users.Client.Models.Responses;

namespace QuickJob.Users.Client.Clients;

public class UsersClient : IUsersClient
{
    private readonly IRequestSender sender;
    private readonly string apiUrl;

    public UsersClient(IRequestSender sender, string apiUrl)
    {
        this.sender = sender;
        this.apiUrl = apiUrl;
    }
        

    public async Task<ApiResult<UserResponse>> GetUserAsync(Guid userId, string accessToken = null) => 
        await sender.SendRequestAsync<UserResponse>("GET", GetUri(ClientPaths.User(userId)), accessToken);

    private string GetUri(string url) => 
        apiUrl + "/" + url;
}