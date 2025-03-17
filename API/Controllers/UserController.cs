using API.Dtos.User;
using API.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddUserDto newUser)
        {
            var response = await _userService.AddAsync(newUser);
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
            var response = await _userService.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "No users found", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            if (!response.Success)
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "User not found", errorType = response.ErrorType });
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDto updatedUser)
        {
            var response = await _userService.UpdateAsync(id, updatedUser);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "User not found", errorType = response.ErrorType });
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
            var response = await _userService.DeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "User not found", errorType = response.ErrorType });
                    case EErrorType.BAD:
                        return BadRequest(new { message = "Bad request", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }
    }
}