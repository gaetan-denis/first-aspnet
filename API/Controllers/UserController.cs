using AutoMapper;

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
            if (!PayloadValidator.ValidateObject(newUser, out string errorMessage))
            {
                var errorResponse = PayloadValidator.BuildError<string>(errorMessage, EErrorType.BAD);
                return BadRequest(errorResponse);
            }


            if (!PayloadValidator.BlockTemporaryEmails(newUser.Email))
            {
                var errorResponse = PayloadValidator.BuildError<string>("Les emails jetables ne sont pas autorisés", EErrorType.BAD);
                return BadRequest(errorResponse);
            }

            var response = await _userService.AddAsync(newUser);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page, [FromQuery] int? window)
        {
            var response = await _userService.GetAllAsync(page, window ?? 20);
            return await HttpManager.HttpResponse(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _userService.GetByIdAsync(id);
            return await HttpManager.HttpResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateUserDto updatedUser)
        {

           if (!PayloadValidator.ValidateObject(updatedUser, out string errorMessage))
            {
                var errorResponse = PayloadValidator.BuildError<string>(errorMessage, EErrorType.BAD);
                return BadRequest(errorResponse);
            }

            // Validation des emails jetables
            if (!PayloadValidator.BlockTemporaryEmails(updatedUser.Email))
            {
                var errorResponse = PayloadValidator.BuildError<string>("Les emails jetables ne sont pas autorisés", EErrorType.BAD);
                return BadRequest(errorResponse);
            }

            var response = await _userService.UpdateAsync(id, updatedUser);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _userService.DeleteAsync(id);
            return await HttpManager.HttpResponse(response);
        }
    }
}