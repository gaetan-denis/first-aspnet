namespace API.Controllers
{
    [Route("API/v1/domains")]
    public class DomainController : ControllerBase
    {
        private readonly IDomainService _domainService;
        public DomainController(IDomainService domainService)
        {
            _domainService = domainService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddDomainDto newDomain)
        {
           if (!PayloadValidator.ValidateObject(newDomain, out string errorMessage))
            {
                var errorResponse = PayloadValidator.BuildError<string>(errorMessage, EErrorType.BAD_REQUEST);
                return BadRequest(errorResponse);
            }
            var response = await _domainService.AddAsync(newDomain);
            return await HttpManager.HttpResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int page, [FromQuery] int? window)
        {
            var response = await _domainService.GetAllAsync(page, window ?? 20);
            return await HttpManager.HttpResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _domainService.GetByIdAsync(id);
            return await HttpManager.HttpResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDomainDto updatedDomain)
        {
           if (!PayloadValidator.ValidateObject(updatedDomain, out string errorMessage))
            {
                var errorResponse = PayloadValidator.BuildError<string>(errorMessage, EErrorType.BAD_REQUEST);
                return BadRequest(errorResponse);
            }
            var response = await _domainService.UpdateAsync(id, updatedDomain);
            return await HttpManager.HttpResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _domainService.DeleteAsync(id);
            return await HttpManager.HttpResponse(response);
        }
    }
}
