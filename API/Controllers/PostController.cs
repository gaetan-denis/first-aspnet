using Microsoft.AspNetCore.Mvc;
using API.Dtos.Post;

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
                return BadRequest(response);
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

                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _postService.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id,[FromBody] UpdatePostDto updatedPost)
        {
            var response = await _postService.UpdateAsync(id, updatedPost);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return NoContent();
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
                return BadRequest(response.Message);
            }
        }
    }
}
