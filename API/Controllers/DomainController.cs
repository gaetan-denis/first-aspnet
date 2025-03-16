using Microsoft.AspNetCore.Mvc;
using API.Dtos.Domain;
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

        public async Task<IActionResult>AddAsync([FromBody] AddDomainDto newDomain)
        {
            var response = await _domainService.AddAsync(newDomain);
            if(response.Success)
            {
                return Ok(response.Data);
            }else
            {
                return BadRequest(response);
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

                return NoContent();
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
                return NotFound();
            }
        }
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id,[FromBody] UpdateDomainDto updatedDomain)
        {
            var response = await _domainService.UpdateAsync(id, updatedDomain);
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
            var response = await _domainService.DeleteAsync(id);
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