namespace API.Services
{
    using API.Dtos.Post;
    
    public interface IPostService
    {
        
        // Retourne une tâche contenant un objet Post.
        Task<ServiceResponse<PostDto>> GetByIdAsync(int id);
        // Récupère un post spécifique en fonction de son id.
        Task<ServiceResponse<IEnumerable<PostDto>>> GetAllAsync();
        //Permet d'ajouter un nouvel post dans le système.
        Task<ServiceResponse<PostDto>> AddAsync(AddPostDto newPost);
        //Permet de modifier un post dans le système.
        Task <ServiceResponse<PostDto>>UpdateAsync(int id, UpdatePostDto updatedPost);
        // Supprime un post dans le système.
        Task<ServiceResponse<PostDto>> DeleteAsync(int id);
    }
}