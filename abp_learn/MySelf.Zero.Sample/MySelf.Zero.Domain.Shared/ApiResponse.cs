namespace MySelf.Zero.Domain.Shared;

public class ApiResponse
{
    public ApiResponseCode Code { get; set; }

    public string Message { get; set; } = string.Empty;

    public bool Success => Code == ApiResponseCode.Succeed;

    public void IsSuccess(string message = "")
    {
        Code = ApiResponseCode.Succeed;
        Message = message;
    }

    public void IsFailed(string message = "")
    {
        Code = ApiResponseCode.Failed;
        Message = message;
    }

    public void IsFailed(Exception exception)
    {
        Code = ApiResponseCode.Failed;
        Message = exception.InnerException?.StackTrace;
    }

}

public class ApiResponse<TResult> : ApiResponse where TResult : class
{
    public TResult Result { get; set; }

    public void IsSuccess(TResult result, string message = "")
    {
        Code = ApiResponseCode.Succeed;
        Message = message;
        Result = result;
    }
}
