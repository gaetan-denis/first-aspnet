
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

        public static Task<IActionResult> HttpResponse<T>(ServiceResponse<T> response) where T : class
        {
            return response.ErrorType switch
            {
                EErrorType.NOTFOUND => Task.FromResult<IActionResult>(new NotFoundObjectResult(response)),
                EErrorType.SUCCESS => Task.FromResult<IActionResult>(new OkObjectResult(response)),
                EErrorType.UNAUTHORIZED => Task.FromResult<IActionResult>(new UnauthorizedObjectResult(response)),
                EErrorType.BAD => Task.FromResult<IActionResult>(new BadRequestObjectResult(response)),
                _ => Task.FromResult<IActionResult>(new BadRequestObjectResult(response)),
            };
        }

    }
}