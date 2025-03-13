using Microsoft.AspNetCore.Mvc;
using API.Dtos.Post;

namespace API.Controllers
{
    
    [Route("API/v1/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Ce controlleur permet l'ajout d'un nouveau post dans la base de données.
        /// </summary>
        /// <param name="newPost">L'objet contenant les informations du post à ajouter, passé en tant que corps de la requête.</param>
        /// <returns>Retourne une réponse HTTP 200 (OK) si l'ajout est réussi, ou une réponse HTTP 400 (BadRequest) en cas d'échec.</returns>

        [HttpPost]

        public async Task<IActionResult> AddAsync([FromBody] AddPostDto newPost)
        {
            var response = await _postService.AddAsync(newPost);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response);
            }
        }
       
        /// <summary>
        /// Ce contrôleur permet de récupérer la liste de tous les posts dans la base de données.
        /// </summary>
        /// <returns>Retourne une réponse HTTP 200 (OK) si la récupération est réussie, ou une réponse HTTP 400 (NoContent)  si aucune donnée n'est trouvée.</returns>
        /// 

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _postService.GetAllAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {

                return NoContent();
            }
        }

        /// <summary>
        /// Ce contrôleur permet de récupérer un post en fonction de son id
        /// </summary>
        /// <param name="id">L'id du post à récupérer</param>
        /// <returns>Retourne une réponse HTTP 200 (OK) si la récupération est réussie, ou une réponse HTTP 404 (NotFound) sile post n'existe pas.</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _postService.GetByIdAsync(id);
            if (response.Success)
            { 
                return Ok(response.Data);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Ce contrôleur permet de modifier un post en fonction de son id
        /// </summary>
        /// <param name="id">L'id du post à modifier</param>
        /// <param name="updatedPost">L'objet contenant les nouvelles informations du post à modifier.</param>
        /// <returns>Retourne une réponse HTTP 200 (OK) si la mise à jour est réussie, ou une réponse HTTP 404 (NotFound) si le post n'existe pas..</returns>

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int id,[FromBody] UpdatePostDto updatedPost)
        {
            var response = await _postService.UpdateAsync(id, updatedPost);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Ce Contrôleur permet de supprimer un post en fonction de son id
        /// </summary>
        /// <param name="id">L'id du post à supprimer </param>
        /// <returns>Retourne une réponse HTTP 204 (NoContent) si la suppression est réussie, ou une réponse HTTP 400 (BadRequest) en cas d'échec.</returns>
            
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var response = await _postService.DeleteAsync(id);
            if (response.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
