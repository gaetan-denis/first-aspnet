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

        /// <summary>
        /// Récupère un domaine en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant unique du domaine à récupérer.</param>
        /// <returns>Un objet ServiceResponse contenant un DomainDto avec les informations du domaine si trouvé, ou une réponse d'erreur si le domaine n'existe pas.</returns>
        
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

        /// <summary>
        /// Récupère tous les domaines.
        /// </summary>
        /// <returns>Un objet ServiceResponse contenant une liste de DomainDto avec les informations de tous les domaines.</returns>
        
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

        /// <summary>
        /// Ajoute un nouveau domaine.
        /// </summary>
        /// <param name="newDomain">Les informations nécessaires pour ajouter un domaine.</param>
        /// <returns>Un objet ServiceResponse contenant un DomainDto avec les informations du domaine ajouté.</returns>
        
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

        /// <summary>
        /// Met à jour un domaine existant.
        /// </summary>
        /// <param name="id">L'identifiant unique du domaine à mettre à jour.</param>
        /// <param name="updatedDomain">Les nouvelles informations du domaine à mettre à jour.</param>
        /// <returns>Un objet ServiceResponse contenant un DomainDto avec les informations du domaine mis à jour.</returns>
        
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

        /// <summary>
        /// Supprime un domaine.
        /// </summary>
        /// <param name="id">L'identifiant unique du domaine à supprimer.</param>
        /// <returns>Un objet ServiceResponse contenant un DomainDto avec les informations du domaine supprimé.</returns>
        
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
