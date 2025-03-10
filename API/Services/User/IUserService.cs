namespace API.Services
{
    using API.Entities;

    public interface IUserService
    {
        // Retourne une tâche contenant un objet User.
        Task<User> GetByIdAsync(int id);
        // Récupère un user spécifique en fonction de son id.
        Task<IEnumerable<User>> GetAllAsync();
        //Permet d'ajouter un nouvel user dans le système.
        Task AddAsync(User user);
        //Permet de modifier un user dans le système.
        Task UpdateAsync(User user);
        // Supprime un user dans le système.
        Task DeleteAsync(int id);
    }
}