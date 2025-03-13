using API.Dtos.User;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService){
            _userService = userService;
        }

        [HttpPost]

        public async Task<IActionResult>AddAsync([FromBody]AddUserDto newUser)
        {
            var response = await _userService.AddAsync(newUser);
            if(response.Success)
            {
                return Ok(response.Data);
            }else
            {
                return BadRequest(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult>GetAllAsync()
        {
            var response = await _userService.GetAllAsync();
            if(response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return NoContent();
            }
        }
        
        [HttpGet ("{id}")]
        public async Task<IActionResult>GetByIdAsync(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            if(response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut ("{id}")]

        public async Task<IActionResult>UpdateAsync(int id)
        {
            var response =await _userService.UpdateAsync(id);
            if(response.Success)
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
            var response = await _userService.DeleteAsync(id);
            if (response.Success)
            {
            return NoContent();
            }else
            {
                return BadRequest(response.Message);
            }
    }
    }
}