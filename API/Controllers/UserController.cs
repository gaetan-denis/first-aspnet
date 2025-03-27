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
            if (
                !PayloadValidator.ProtectAgainstSQLI(newUser.Email) ||
                !PayloadValidator.ProtectAgainstSQLI(newUser.Username) ||
                !PayloadValidator.ProtectAgainstSQLI(newUser.Password))
            {
                var errorResponse = PayloadValidator.BuildError<string>("Tentative de soumettre des données invalides. Les entrées ne sont pas autorisées.", EErrorType.BAD);
                return BadRequest(errorResponse);
            }
            if (
                !PayloadValidator.ProtectAgainstXSS(newUser.Email) ||
                !PayloadValidator.ProtectAgainstXSS(newUser.Username) ||
                !PayloadValidator.ProtectAgainstXSS(newUser.Password))
            {
                var errorResponse = PayloadValidator.BuildError<string>("Caractères dangereux détectés dans l'entrée.", EErrorType.BAD);
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

            if (
                !PayloadValidator.ProtectAgainstSQLI(updatedUser.Email) ||
                !PayloadValidator.ProtectAgainstSQLI(updatedUser.Username) ||
                !PayloadValidator.ProtectAgainstSQLI(updatedUser.Password))
            {
                var errorResponse = PayloadValidator.BuildError<string>("Tentative de soumettre des données invalides. Les entrées ne sont pas autorisées.", EErrorType.BAD);
                return BadRequest(errorResponse);
            }

            // Protection contre les XSS
            if (
                !PayloadValidator.ProtectAgainstXSS(updatedUser.Email) ||
                !PayloadValidator.ProtectAgainstXSS(updatedUser.Username) ||
                !PayloadValidator.ProtectAgainstXSS(updatedUser.Password))
            {
                var errorResponse = PayloadValidator.BuildError<string>("Caractères dangereux détectés dans l'entrée.", EErrorType.BAD);
                return BadRequest(errorResponse);
            }

            // Validation des emails jetables
            if (!PayloadValidator.BlockTemporaryEmails(updatedUser.Email))
            {
                var errorResponse = PayloadValidator.BuildError<string>("Les emails jetables ne sont pas autorisés", EErrorType.BAD);
                return BadRequest(errorResponse);
            }

            // Appel au service pour mettre à jour l'utilisateur
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