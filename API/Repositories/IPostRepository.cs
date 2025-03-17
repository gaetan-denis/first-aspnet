namespace API.Repositories
{
    public interface IPostRepository
    {
        // Récupération d'un post par son ID
        Task<Post?> GetByIdAsync(int id);
        //Récupérer tous les posts
        Task<IEnumerable<Post>> GetAllAsync();
        //Ajouter un post
        Task<Post> AddAsync(Post post);
        // Modifier un post
        Task<Post> UpdateAsync(Post post);
        //delete un poost
        Task DeleteAsync(int id);
    }
}