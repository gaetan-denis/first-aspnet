using API.Controllers;
using API.Dtos.Domain;
using API.Enums;
using API.Repositories;

namespace API.Services
{
    public class DomainService : IDomainService
    {
        private readonly IDomainRepository _domainRepository;

        public DomainService(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        // Récupérer un domain par ID
        public async Task<ServiceResponse<DomainDto>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<DomainDto>();
            var domain = await _domainRepository.GetByIdAsync(id);
            if (domain == null)
            {
                return HttpManager.CreateErrorResponse<DomainDto>(EErrorType.NOTFOUND, "domaine non trouvé");
            }

            response.Data = new DomainDto
            {
                Name = domain.Name,

            };
             return HttpManager.CreateSuccessResponse(response.Data);
        }

        // Récupérer tous les domains
        public async Task<ServiceResponse<IEnumerable<DomainDto>>> GetAllAsync()
        {
            var response = new ServiceResponse<IEnumerable<DomainDto>>();

            var domains = await _domainRepository.GetAllAsync();
            response.Data = domains.Select(d => new DomainDto
            {
                Name = d.Name,

            }).ToList();

             return HttpManager.CreateSuccessResponse(response.Data);
        }

        // Ajouter un domain
        public async Task<ServiceResponse<DomainDto>> AddAsync(AddDomainDto newDomain)
        {
            var response = new ServiceResponse<DomainDto>();

            var domain = new Domain
            {
                Name = newDomain.Name,
            };

            var addedDomain = await _domainRepository.AddAsync(domain);

            response.Data = new DomainDto
            {
                Name = addedDomain.Name,
            };

             return HttpManager.CreateSuccessResponse(response.Data);
        }

        // Mettre à jour un domain
        public async Task<ServiceResponse<DomainDto>> UpdateAsync(int id, UpdateDomainDto updatedDomain)
        {
            var response = new ServiceResponse<DomainDto>();

            var existingDomain = await _domainRepository.GetByIdAsync(id);
            if (existingDomain == null)
            {
                return HttpManager.CreateErrorResponse<DomainDto>(EErrorType.NOTFOUND, "domaine non trouvé");
            }

            existingDomain.Name = updatedDomain.Name;

            var updated = await _domainRepository.UpdateAsync(existingDomain);

            response.Data = new DomainDto
            {
                Name = updated.Name,
            };

            return HttpManager.CreateSuccessResponse(response.Data);
        }

        // Supprimer un domain
        public async Task<ServiceResponse<DomainDto>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<DomainDto>();

            var domain = await _domainRepository.GetByIdAsync(id);
            if (domain == null)
            {
                return HttpManager.CreateErrorResponse<DomainDto>(EErrorType.NOTFOUND, "domaine non trouvé");
            }

            await _domainRepository.DeleteAsync(id);

            response.Data = new DomainDto
            {
                Name = domain.Name,
            };

             return HttpManager.CreateSuccessResponse(response.Data);
        }
    }
}
