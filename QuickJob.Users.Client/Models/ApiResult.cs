namespace QuickJob.Users.Client.Models;

public class ApiResult
{
    private static ApiResult successfulResult(int code) => new()
    {
        StatusCode = code
    };

    protected ApiResult(ErrorResult errorResult = null)
    {
        ErrorResult = errorResult;
    }

    public bool IsSuccessful => ErrorResult == null;
    public int StatusCode { get; set; }
    public ErrorResult ErrorResult { get; }

    public bool HasErrorCode(int code) => !IsSuccessful && ErrorResult.ErrorCode.Equals(code);

    public static ApiResult CreateSuccessful(int code) => successfulResult(code);

    public static ApiResult CreateError(ErrorResult error) => new(error);
}

public class ApiResult<TResponse> : ApiResult
{
    private ApiResult(TResponse response = default, int? code = null, ErrorResult error = null) : base(error)
    {
        StatusCode = code ?? 0;
        Response = response;
    }

    public TResponse Response { get; }

    public static ApiResult<TResponse> CreateSuccessful(int code, TResponse response) => new(response, code);
    public new static ApiResult<TResponse> CreateError(ErrorResult error) => new(default, null, error);
}

public class ErrorResult
{
    public ErrorResult(string errorMessage, int errorCode)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
    }

    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}


