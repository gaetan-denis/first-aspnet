
namespace API.Repositories
{
    public interface IUserRepository
    {
        // Récupération d'un utilisateur par son ID
        Task<User> GetByIdAsync(int id);
        //Récupérer tous les utilisateurs
        Task<IEnumerable<User>> GetAllAsync();
        //Ajouter un user
        Task<User> AddAsync(User user);
        // Modifier un user
        Task<User> UpdateAsync(User user);
        //delete un user
        Task DeleteAsync(int id);
    }
}