using System;
using System.Threading.Tasks;
using QuickJob.Users.Client.Models;
using QuickJob.Users.Client.Models.Responses;

namespace QuickJob.Users.Client.Clients;

public interface IUsersClient
{
    Task<ApiResult<UserResponse>> GetUserAsync(Guid userId, string accessToken = null);
}
