using Microsoft.AspNetCore.Mvc;
using API.Dtos.Post;
using API.Enums;
using API.Validator;

namespace API.Controllers
{
    [Route("API/v1/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddPostDto newPost)
        {
            if (
                !PayloadValidator.ProtectAgainstSQLI(newPost.Title) ||
                !PayloadValidator.ProtectAgainstSQLI(newPost.Content))                 
            {
                var errorResponse = PayloadValidator.BuildError<string>("Tentative de soumettre des données invalides. Les entrées ne sont pas autorisées.", EErrorType.BAD);
                return BadRequest(errorResponse);
            }
            if (
                !PayloadValidator.ProtectAgainstXSS(newPost.Title) ||
                !PayloadValidator.ProtectAgainstXSS(newPost.Content))
                
            {
                var errorResponse = PayloadValidator.BuildError<string>("Caractères dangereux détectés dans l'entrée.", EErrorType.BAD);
                return BadRequest(errorResponse);
            }

            var response = await _postService.AddAsync(newPost);
             return await HttpManager.HttpResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page, [FromQuery] int? window)
        {
            var response = await _postService.GetAllAsync(page, window ?? 20);
            return await HttpManager.HttpResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _postService.GetByIdAsync(id);
            return await HttpManager.HttpResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePostDto updatedPost)
        {
             if (
                !PayloadValidator.ProtectAgainstSQLI(updatedPost.Title) ||
                !PayloadValidator.ProtectAgainstSQLI(updatedPost.Content))                 
            {
                var errorResponse = PayloadValidator.BuildError<string>("Tentative de soumettre des données invalides. Les entrées ne sont pas autorisées.", EErrorType.BAD);
                return BadRequest(errorResponse);
            }
            if (
                !PayloadValidator.ProtectAgainstXSS(updatedPost.Title) ||
                !PayloadValidator.ProtectAgainstXSS(updatedPost.Content))
                
            {
                var errorResponse = PayloadValidator.BuildError<string>("Caractères dangereux détectés dans l'entrée.", EErrorType.BAD);
                return BadRequest(errorResponse);
            }
            var response = await _postService.UpdateAsync(id, updatedPost);
             return await HttpManager.HttpResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _postService.DeleteAsync(id);
             return await HttpManager.HttpResponse(response);
        }
    }
}
