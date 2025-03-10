namespace API.Services
{
    using API.Entities;
    public interface IPostService
    {
        
        // Retourne une tâche contenant un objet Post.
        Task<Post> GetByIdAsync(int id);
        // Récupère un post spécifique en fonction de son id.
        Task<IEnumerable<Post>> GetAllAsync();
        //Permet d'ajouter un nouvel post dans le système.
        Task AddAsync(Post post);
        //Permet de modifier un post dans le système.
        Task UpdateAsync(Post post);
        // Supprime un post dans le système.
        Task DeleteAsync(int id);
    }
}