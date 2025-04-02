using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using System.Reflection;

namespace API.Controllers
{
    public static class HttpManager
    {
        /// <summary>
        /// Cette méthode récupère le nom du contrôleur à partir des données de la route HTTP.
        /// </summary>
        /// <returns>Le nom du controleur ou "UnknownController</returns>
        private static string GetControllerName()
        {
            var routeData = new HttpContextAccessor().HttpContext?.GetRouteData();
            return routeData?.Values["controller"]?.ToString() ?? "UnknownController";
        }

        /// <summary>
        ///  Cette méthode récupère la valeur de l'ID ou de la clé primaire d'une entité donnée. 
        /// Elle s'attend à ce que l'entité possède une propriété nommée "Id","UserId","PostId" ou "DomaineId".
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns>null si l'entité est null ou une propriété similaires à la description</returns>
        /// 
        private static object GetEntityId<T>(T entity)
        {
            if (entity == null) return null;

            var properties = typeof(T).GetProperties();
            var idProperty = properties.FirstOrDefault(p =>
                p.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                p.Name.Equals("UserId", StringComparison.OrdinalIgnoreCase) ||
                p.Name.Equals("PostId", StringComparison.OrdinalIgnoreCase) ||
                p.Name.Equals("DomainId", StringComparison.OrdinalIgnoreCase)
            );

            return idProperty?.GetValue(entity);
        }

        /// <summary>
        /// Cette méthode génère une réponse HTTP appropriée pour une action de suppression, en fonction du succès ou de l'échec de l'opération.
        /// </summary>
        /// <param name="response"></param>
        /// <returns>Si Success est true, elle retroune un NoContentResult, Si false elle génère une erreur via HrrpResponse</returns>

        public static Task<IActionResult> CreateDeleteResponse<T>(ServiceResponse<T> response) where T : class
        {
            Console.WriteLine($"Delete Response - Success: {response.Success}, ErrorType: {response.ErrorType}");
            return response.Success ? Task.FromResult<IActionResult>(new NoContentResult()) : HttpResponse(response);  
        }

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
                EErrorType.CREATED => Task.FromResult<IActionResult>(new CreatedAtActionResult("GetByIdAsync", GetControllerName(), new { id = GetEntityId(response.Data) }, response)),
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
