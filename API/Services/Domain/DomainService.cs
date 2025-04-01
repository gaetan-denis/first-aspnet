using AutoMapper;

namespace API.Services
{
    public class DomainService : IDomainService
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IMapper _mapper;

        public DomainService(IDomainRepository domainRepository, IMapper mapper)
        {
            _domainRepository = domainRepository;
            _mapper = mapper;
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
                return HttpManager.CreateErrorResponse<DomainDto>(EErrorType.NOT_FOUND, "domaine non trouvé");
            }

            response.Data = _mapper.Map<DomainDto>(domain);
            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Récupère tous les domaines.
        /// </summary>
        /// <returns>Un objet ServiceResponse contenant une liste de DomainDto avec les informations de tous les domaines.</returns>

        public async Task<ServiceResponse<Pagination<DomainDto>>> GetAllAsync(int page, int window)
        {
            var response = new ServiceResponse<Pagination<DomainDto>>();

            // Récupérer tous les domaines
            var domains = await _domainRepository.GetAllAsync();

            // Si aucun domaine n'est trouvé
            if (domains == null || !domains.Any())
            {
                return HttpManager.CreateErrorResponse<Pagination<DomainDto>>(EErrorType.NOT_FOUND, "Aucun domaine trouvé");
            }

            // Pagination
            int totalDomains = domains.Count();  // Total des domaines
            var paginatedDomains = domains
                .Skip((page - 1) * window)      // Calculer le décalage pour la page courante
                .Take(window)                  // Prendre un nombre de domaines défini par la fenêtre
                .ToList();

            // Mapper les domaines en DomainDto
            var domainDtos = _mapper.Map<List<DomainDto>>(paginatedDomains);
            // Créer la réponse avec la pagination
            response.Data = new Pagination<DomainDto>
            {
                Data = domainDtos,
                Page = page,
                Total = totalDomains
            };

            // Retourner la réponse réussie
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

            response.Data = _mapper.Map<DomainDto>(addedDomain);


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
                return HttpManager.CreateErrorResponse<DomainDto>(EErrorType.NOT_FOUND, "domaine non trouvé");
            }

            existingDomain.Name = updatedDomain.Name;

            var updated = await _domainRepository.UpdateAsync(existingDomain);

            response.Data = _mapper.Map<DomainDto>(updated);


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
                return HttpManager.CreateErrorResponse<DomainDto>(EErrorType.NOT_FOUND, "domaine non trouvé");
            }

            await _domainRepository.DeleteAsync(id);

            response.Data = _mapper.Map<DomainDto>(domain);

            return HttpManager.CreateSuccessResponse(response.Data);
        }
    }
}
