using System;
using System.Threading.Tasks;
using QuickJob.Users.Client.Models;
using QuickJob.Users.Client.Models.Responses;

namespace QuickJob.Users.Client.Clients;

public class UsersClient : IUsersClient
{
    private readonly IRequestSender sender;

    public UsersClient(IRequestSender sender)
    {
        this.sender = sender;
    }

    public async Task<ApiResult<UserResponse>> GetUserAsync(Guid userId, string accessToken = null) => 
        await sender.SendRequestAsync<UserResponse>("GET", ClientPaths.User(userId), accessToken);
}