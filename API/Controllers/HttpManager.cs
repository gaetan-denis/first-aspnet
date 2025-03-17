
using API.Enums;

namespace API.Controllers
{
    public static class HttpManager
{
    public static ServiceResponse<T> CreateErrorResponse<T>(EErrorType errorType, string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            ErrorType = errorType,
            Message = message,
        };
    }

    public static ServiceResponse<T> CreateSuccessResponse<T>(T data)
    {
        return new ServiceResponse<T>
        {
            Success = true,
            Data = data
        };
    }
}
}