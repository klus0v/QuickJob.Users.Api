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

    public async Task<ApiResult<UserRepresentation>> GetUserAsync(Guid userId) => 
        await sender.SendRequestAsync<UserRepresentation>("GET", ClientPaths.User(userId));

    public async Task<ApiResult<UserRepresentation>> PatchUserAsync(Guid userId, UserRepresentation request) => 
        await sender.SendRequestAsync<UserRepresentation>("PATCH", ClientPaths.User(userId), request);

    public async Task<ApiResult> DeleteUserAsync(Guid userId) => 
        await sender.SendRequestAsync("DELETE", ClientPaths.User(userId));
}