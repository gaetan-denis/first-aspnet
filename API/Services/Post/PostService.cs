using API.Controllers;
using API.Dtos.Post;
using API.Enums;
using API.Repositories;

namespace API.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        
        // Permet d'associer les utilisateurs
        private readonly IUserRepository _userRepository;

        public PostService(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            
        }

        // Récupérer un domain par ID
        public async Task<ServiceResponse<PostDto>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<PostDto>();
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND,"post non trouvé");
            }

            response.Data = new PostDto
            {
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId

            };
             return HttpManager.CreateSuccessResponse(response.Data);
        }

        // Récupérer tous les domains
        public async Task<ServiceResponse<IEnumerable<PostDto>>> GetAllAsync()
        {
            var response = new ServiceResponse<IEnumerable<PostDto>>();

            var posts = await _postRepository.GetAllAsync();
            response.Data = posts.Select(p => new PostDto
            {
                Title = p.Title,
                Content = p.Content,
                UserId = p.UserId

            }).ToList();

             return HttpManager.CreateSuccessResponse(response.Data);
        }

        // Ajouter un domain
        public async Task<ServiceResponse<PostDto>> AddAsync(AddPostDto newPost)
        {
            var response = new ServiceResponse<PostDto>();

            // Permet de récupérer l'utilisateur associé à UserID
            var user = await _userRepository.GetByIdAsync(newPost.UserId);
            if (user == null)
            {
                return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND,"utilisateur non trouvé");
            }


            var post = new Post
            {
                UserId = newPost.UserId,
                Title = newPost.Title,
                Content = newPost.Content,
                CreatedAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                // Permet d'associer l'utilisateur trouvé
                User = user,
            };

            var addedPost = await _postRepository.AddAsync(post);

            response.Data = new PostDto
            {
                Title = addedPost.Title,
                Content = addedPost.Content,
                UserId = addedPost.UserId
            };

             return HttpManager.CreateSuccessResponse(response.Data);
        }

        // Mettre à jour un domain
        public async Task<ServiceResponse<PostDto>> UpdateAsync(int id, UpdatePostDto updatedPost)
{
    var response = new ServiceResponse<PostDto>();

    var existingPost = await _postRepository.GetByIdAsync(id);
    
    if (existingPost == null)
    {
        return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND, "Post non trouvé");
    }


    // Vérifie si l'utilisateur existe
    var user = await _userRepository.GetByIdAsync(updatedPost.UserId);
    if (user == null)
    {
        return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND, "Utilisateur non trouvé");
    }

    existingPost.Title = updatedPost.Title;
    existingPost.Content = updatedPost.Content;
    existingPost.User = user; // Met à jour l'utilisateur
    existingPost.UserId = updatedPost.UserId;

    var updated = await _postRepository.UpdateAsync(existingPost);

    response.Data = new PostDto
    {
        Title = updated.Title,
        Content = updated.Content,
        UserId = updated.UserId
    };

     return HttpManager.CreateSuccessResponse(response.Data);
}

        // Supprimer un domain
        public async Task<ServiceResponse<PostDto>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<PostDto>();

            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
               return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND,"post non trouvé");
            }

            await _postRepository.DeleteAsync(id);

            response.Data = new PostDto
            {
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
            };

             return HttpManager.CreateSuccessResponse(response.Data);
        }
    }
}
