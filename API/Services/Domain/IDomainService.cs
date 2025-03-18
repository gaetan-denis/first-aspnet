namespace API.Services
{
    using API.Dtos.Domain;
    using API.Dtos.Responses;

    public interface IDomainService
    {
        // Retourne une tâche contenant un objet Domain.
        Task<ServiceResponse<DomainDto>> GetByIdAsync(int id);
        // Récupère un domaine spécifique en fonction de son id.
        Task<ServiceResponse<Pagination<DomainDto>>> GetAllAsync(int page, int window);
        //Permet d'ajouter un nouvel domaine dans le système.
        Task<ServiceResponse<DomainDto>> AddAsync(AddDomainDto newDomain);
        //Permet de modifier un domain dans le système.
        Task <ServiceResponse<DomainDto>>UpdateAsync(int id, UpdateDomainDto updateddomain);
        // Supprime un domain dans le système.
        Task <ServiceResponse<DomainDto>>DeleteAsync(int id);
    }
}  