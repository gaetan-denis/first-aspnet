using Microsoft.AspNetCore.Mvc;
using API.Dtos.Post;
using API.Enums;

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
            var response = await _postService.AddAsync(newPost);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.BAD:
                        return BadRequest(new { message = "Bad request", errorType = response.ErrorType });
                    case EErrorType.CONFLICT:
                        return Conflict(new { message = "Conflict occurred", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _postService.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "No posts found", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _postService.GetByIdAsync(id);
            if (!response.Success)
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "Post not found", errorType = response.ErrorType });
                    case EErrorType.UNAUTHORIZED:
                        return Unauthorized(new { message = "Unauthorized access", errorType = response.ErrorType });
                    case EErrorType.BAD:
                        return BadRequest(new { message = "Bad request", errorType = response.ErrorType });
                    case EErrorType.CONFLICT:
                        return Conflict(new { message = "Conflict occurred", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }

            return Ok(response.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdatePostDto updatedPost)
        {
            var response = await _postService.UpdateAsync(id, updatedPost);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "Post not found", errorType = response.ErrorType });
                    case EErrorType.BAD:
                        return BadRequest(new { message = "Bad request", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _postService.DeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "Post not found", errorType = response.ErrorType });
                    case EErrorType.BAD:
                        return BadRequest(new { message = "Bad request", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }
    }
}
