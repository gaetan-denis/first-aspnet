namespace API.Repositories
{
    public interface IDomainRepository
    {
        //Récupérer un domain par id
        Task<Domain?> GetByIdAsync(int id);
        //Récupérer tous les domains
        Task<IEnumerable<Domain>> GetAllAsync();
        //Ajouter un domain
        Task<Domain> AddAsync(Domain domain);
        // Modifier un domain
        Task<Domain> UpdateAsync(Domain domain);
        //delete un domain
        Task DeleteAsync(int id);
    }
}