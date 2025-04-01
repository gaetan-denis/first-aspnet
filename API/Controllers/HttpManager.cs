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
                EErrorType.NOT_FOUND => Task.FromResult<IActionResult>(new NotFoundObjectResult(response)),
                EErrorType.SUCCESS => Task.FromResult<IActionResult>(new OkObjectResult(response)),
                EErrorType.CREATED => Task.FromResult<IActionResult>(new CreatedAtActionResult("GetItem", "Controller", null, response)),
                EErrorType.NOT_CONTENT => Task.FromResult<IActionResult>(new NoContentResult()),
                EErrorType.UNAUTHORIZED => Task.FromResult<IActionResult>(new UnauthorizedObjectResult(response)),
                EErrorType.BAD_REQUEST => Task.FromResult<IActionResult>(new BadRequestObjectResult(response)),
                EErrorType.UNPROCESSABLE_ENTITY => Task.FromResult<IActionResult>(new UnprocessableEntityObjectResult(response)),
                EErrorType.CONFLICT => Task.FromResult<IActionResult>(new ConflictObjectResult(response)),
                EErrorType.INTERNAL_SERVER_ERROR => Task.FromResult<IActionResult>(new StatusCodeResult(500)),
                EErrorType.SERVICE_UNAVAILABLE => Task.FromResult<IActionResult>(new StatusCodeResult(503)),
                EErrorType.METHOD_NOT_ALLOWED => Task.FromResult<IActionResult>(new StatusCodeResult(405)),
                _ => Task.FromResult<IActionResult>(new BadRequestObjectResult(response)),
            };
        }
    }
}
