using System.Threading.Tasks;
using QuickJob.Users.Client.Models;

namespace QuickJob.Users.Client;

public interface IRequestSender
{
    Task<ApiResult<TResponse>> SendRequestAsync<TResponse>(
        string httpMethod,
        string uri,
        string accessToken = null,
        object requestEntity = null)
        where TResponse : class;

    Task<ApiResult> SendRequestAsync(
        string httpMethod,
        string uri,
        string accessToken = null,
        object requestEntity = null);
}
