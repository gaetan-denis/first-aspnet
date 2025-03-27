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

        /// <summary>
        /// Récupère un post par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant unique du post à récupérer.</param>
        /// <returns>Un objet ServiceResponse contenant un PostDto avec les détails du post si trouvé, ou une réponse d'erreur si le post n'existe pas.</returns>  

        public async Task<ServiceResponse<PostDto>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<PostDto>();
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND, "post non trouvé");
            }

            response.Data = new PostDto
            {
                PostId = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId

            };
            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Récupère tous les posts disponibles.
        /// </summary>
        /// <returns>Un objet ServiceResponse contenant une liste de PostDto.</returns>

        public async Task<ServiceResponse<Pagination<PostDto>>> GetAllAsync(int page, int window)
        {
            var response = new ServiceResponse<Pagination<PostDto>>();

            
            var posts = await _postRepository.GetAllAsync();

            
            if (posts == null || !posts.Any())
            {
                return HttpManager.CreateErrorResponse<Pagination<PostDto>>(EErrorType.NOTFOUND, "Aucun post trouvé");
            }

            
            int totalPosts = posts.Count();  
            var paginatedPosts = posts
                .Skip((page - 1) * window)      
                .Take(window)                  
                .ToList();

            
            var postDtos = paginatedPosts.Select(p => new PostDto
            {
                PostId = p.Id,
                Title = p.Title,
                Content = p.Content,
                UserId = p.UserId
            }).ToList();

            
            response.Data = new Pagination<PostDto>
            {
                Data = postDtos,
                Page = page,
                Total = totalPosts
            };

            
            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Ajoute un nouveau post.
        /// </summary>
        /// <param name="newPost">Les informations du post à ajouter.</param>
        /// <returns>Un objet ServiceResponse contenant le PostDto du post ajouté.</returns>

        public async Task<ServiceResponse<PostDto>> AddAsync(AddPostDto newPost)
        {
            var response = new ServiceResponse<PostDto>();

            // Permet de récupérer l'utilisateur associé à UserID
            var user = await _userRepository.GetByIdAsync(newPost.UserId);
            if (user == null)
            {
                return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND, "utilisateur non trouvé");
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
                PostId = post.Id,
                Title = addedPost.Title,
                Content = addedPost.Content,
                UserId = addedPost.UserId
            };

            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Met à jour un post existant.
        /// </summary>
        /// <param name="id">L'identifiant du post à mettre à jour.</param>
        /// <param name="updatedPost">Les nouvelles informations du post.</param>
        /// <returns>Un objet ServiceResponse contenant le PostDto mis à jour.</returns>

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
            existingPost.UpdateAt = DateTime.UtcNow;

            var updated = await _postRepository.UpdateAsync(existingPost);

            response.Data = new PostDto
            {
                PostId = updated.Id,
                Title = updated.Title,
                Content = updated.Content,
                UserId = updated.UserId
            };

            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Supprime un post par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant du post à supprimer.</param>
        /// <returns>Un objet ServiceResponse contenant les informations du post supprimé.</returns>

        public async Task<ServiceResponse<PostDto>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<PostDto>();

            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return HttpManager.CreateErrorResponse<PostDto>(EErrorType.NOTFOUND, "post non trouvé");
            }

            await _postRepository.DeleteAsync(id);

            response.Data = new PostDto
            {
                PostId = post.Id,
                Title = post.Title,
                Content = post.Content,
                UserId = post.UserId,
            };

            return HttpManager.CreateSuccessResponse(response.Data);
        }
    }
}
