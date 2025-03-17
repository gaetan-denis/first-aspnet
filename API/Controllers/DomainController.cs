using Microsoft.AspNetCore.Mvc;
using API.Dtos.Domain;
using API.Enums;
using API.Services;

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
            var response = await _domainService.AddAsync(newDomain);
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
            var response = await _domainService.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "No domains found", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _domainService.GetByIdAsync(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "Domain not found", errorType = response.ErrorType });
                    case EErrorType.BAD:
                        return BadRequest(new { message = "Bad request", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateDomainDto updatedDomain)
        {
            var response = await _domainService.UpdateAsync(id, updatedDomain);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "Domain not found", errorType = response.ErrorType });
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
            var response = await _domainService.DeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                switch (response.ErrorType)
                {
                    case EErrorType.NOTFOUND:
                        return NotFound(new { message = "Domain not found", errorType = response.ErrorType });
                    case EErrorType.BAD:
                        return BadRequest(new { message = "Bad request", errorType = response.ErrorType });
                    default:
                        return StatusCode(500, new { message = "An unexpected error occurred", errorType = response.ErrorType });
                }
            }
        }
    }
}
