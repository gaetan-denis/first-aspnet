namespace API.Controllers
{
    public static class HttpManager
    {
        // Crée une réponse d'erreur
        public static ServiceResponse<T> CreateErrorResponse<T>(EErrorType errorType, string message)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                ErrorType = errorType,
                Message = message,
            };
        }

        // Crée une réponse de succès
        public static ServiceResponse<T> CreateSuccessResponse<T>(T data)
        {
            return new ServiceResponse<T>
            {
                Success = true,
                Data = data
            };
        }

        // Transforme la réponse du service en une réponse HTTP
        public static Task<IActionResult> HttpResponse<T>(ServiceResponse<T> response) where T : class
        {
            // On détermine la réponse HTTP en fonction du type d'erreur dans la réponse
            return response.ErrorType switch
            {
                EErrorType.NOTFOUND => Task.FromResult<IActionResult>(new NotFoundObjectResult(response)),  // 404 Not Found
                EErrorType.SUCCESS => Task.FromResult<IActionResult>(new OkObjectResult(response)),  // 200 OK
                EErrorType.UNAUTHORIZED => Task.FromResult<IActionResult>(new UnauthorizedObjectResult(response)),  // 401 Unauthorized
                EErrorType.BAD => Task.FromResult<IActionResult>(new BadRequestObjectResult(response)),  // 400 Bad Request
                EErrorType.VALIDATION_ERROR => Task.FromResult<IActionResult>(new UnprocessableEntityObjectResult(response)),  // 422 Unprocessable Entity
                EErrorType.CONFLICT => Task.FromResult<IActionResult>(new ConflictObjectResult(response)),  // 409 Conflict
                EErrorType.INTERNAL_SERVER_ERROR => Task.FromResult<IActionResult>(new StatusCodeResult(500)),  // 500 Internal Server Error
                EErrorType.SERVICE_UNAVAILABLE => Task.FromResult<IActionResult>(new StatusCodeResult(503)),  // 503 Service Unavailable
                EErrorType.METHOD_NOT_ALLOWED => Task.FromResult<IActionResult>(new StatusCodeResult(405)),  // 405 Method Not Allowed
                _ => Task.FromResult<IActionResult>(new BadRequestObjectResult(response)),  // Par défaut, 400 Bad Request
            };
        }
    }
}
