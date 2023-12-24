using System;
using System.Threading.Tasks;
using QuickJob.Users.DataModel.Api.Responses.Users;

namespace QuickJob.Users.Client.Clients;

public interface IUsersClient
{
    Task<ApiResult<UserResponse>> GetUserAsync(Guid userId, string accessToken = null);
}
