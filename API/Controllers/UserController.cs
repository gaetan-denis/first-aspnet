using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService){
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult>GetAllAsync()
        {
            var response = await _userService.GetAllAsync();
            if(response.Success){
                return Ok(response.Data);
            }else{
                return NoContent();
            }
        }

        [HttpGet ("{Id}")]
        public async Task<IActionResult>GetByIdAsync(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            if(response.Success){
                return Ok(response.Data);
            }else{
                return NoContent();
            }
        }
    }
}