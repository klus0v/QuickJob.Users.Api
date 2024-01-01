using System;
using System.Threading.Tasks;
using FS.Keycloak.RestApiClient.Model;
using QuickJob.Users.Client.Models;

namespace QuickJob.Users.Client.Clients;

public class UsersClient : IUsersClient
{
    private readonly IRequestSender sender;

    public UsersClient(IRequestSender sender)
    {
        this.sender = sender;
    }

    public async Task<ApiResult<UserRepresentation>> GetUserAsync(Guid userId, string accessToken = null) => 
        await sender.SendRequestAsync<UserRepresentation>("GET", ClientPaths.User(userId), accessToken);
}