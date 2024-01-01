using System;
using System.Threading.Tasks;
using FS.Keycloak.RestApiClient.Model;
using QuickJob.Users.Client.Models;

namespace QuickJob.Users.Client.Clients;

public interface IUsersClient
{
    Task<ApiResult<UserRepresentation>> GetUserAsync(Guid userId, string accessToken = null);
}
