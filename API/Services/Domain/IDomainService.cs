namespace API.Services
{
    using API.Dtos.Responses;
    using API.Entities;
   public interface IDomainRepository
    {
        // Retourne une tâche contenant un objet Domain.
        Task<ServiceResponse<Domain>> GetByIdAsync(int id);
        // Récupère un domaine spécifique en fonction de son id.
        Task<IEnumerable<Domain>> GetAllAsync();
        //Permet d'ajouter un nouvel domaine dans le système.
        Task AddAsync(Domain domain);
        //Permet de modifier un domain dans le système.
        Task UpdateAsync(Domain domain);
        // Supprime un domain dans le système.
        Task DeleteAsync(int id);
    }
}  