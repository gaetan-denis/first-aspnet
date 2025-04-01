using AutoMapper;

namespace API.Services
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordManager passwordManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordManager = passwordManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Récupère un utilisateur en fonction de son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'utilisateur à récupérer.</param>
        /// <returns>Un objet ServiceResponse contenant un UserDto avec les informations de l'utilisateur si trouvé, ou une réponse d'erreur si l'utilisateur n'existe pas.</returns>

        public async Task<ServiceResponse<UserDto>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<UserDto>();
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return HttpManager.CreateErrorResponse<UserDto>(EErrorType.NOT_FOUND, "utilisateur non trouvé");
            }

            response.Data = _mapper.Map<UserDto>(user);
            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Récupère tous les utilisateurs.
        /// </summary>
        /// <returns>Un objet ServiceResponse contenant une liste de UserDto avec les informations de tous les utilisateurs.</returns>

        public async Task<ServiceResponse<Pagination<UserDto>>> GetAllAsync(int page, int window)
        {
            var response = new ServiceResponse<Pagination<UserDto>>();

            var users = await _userRepository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return HttpManager.CreateErrorResponse<Pagination<UserDto>>(EErrorType.NOT_FOUND, "Utilisateurs non trouvés");
            }


            int totalUsers = users.Count();
            var paginatedUsers = users
                .Skip((page - 1) * window)
                .Take(window)
                .ToList();

            var userDtos = _mapper.Map<List<UserDto>>(paginatedUsers);

            response.Data = new Pagination<UserDto>
            {
                Data = userDtos,
                Page = page,
                Total = totalUsers
            };

            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Ajoute un nouvel utilisateur.
        /// </summary>
        /// <param name="newUser">Les informations nécessaires pour ajouter un utilisateur.</param>
        /// <returns>Un objet ServiceResponse contenant un UserDto avec les informations de l'utilisateur ajouté.</returns>

        public async Task<ServiceResponse<UserDto>> AddAsync(AddUserDto newUser)
        {
            var response = new ServiceResponse<UserDto>();

            // Hachage du mot de passer avec génération du sel

            string salt;
            var hashedPassword = _passwordManager.HashPassword(newUser.Password, out salt);

            var user = new User
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Password = hashedPassword,
                Salt = salt,
                IsAdmin = newUser.IsAdmin,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var addedUser = await _userRepository.AddAsync(user);

            response.Data = _mapper.Map<UserDto>(addedUser);

            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Met à jour un utilisateur existant.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'utilisateur à mettre à jour.</param>
        /// <param name="updatedUser">Les nouvelles informations de l'utilisateur à mettre à jour.</param>
        /// <returns>Un objet ServiceResponse contenant un UserDto avec les informations de l'utilisateur mis à jour.</returns>

        public async Task<ServiceResponse<UserDto>> UpdateAsync(int id, UpdateUserDto updatedUser)
        {
            var response = new ServiceResponse<UserDto>();


            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {

                return HttpManager.CreateErrorResponse<UserDto>(EErrorType.NOT_FOUND, "utilisateur non trouvé");
            }
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;

            var updated = await _userRepository.UpdateAsync(existingUser);


            response.Data = _mapper.Map<UserDto>(updated);

            return HttpManager.CreateSuccessResponse(response.Data);
        }

        /// <summary>
        /// Supprime un utilisateur.
        /// </summary>
        /// <param name="id">L'identifiant unique de l'utilisateur à supprimer.</param>
        /// <returns>Un objet ServiceResponse contenant un UserDto avec les informations de l'utilisateur supprimé.</returns>

        public async Task<ServiceResponse<UserDto>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<UserDto>();

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return HttpManager.CreateErrorResponse<UserDto>(EErrorType.NOT_FOUND, "utilisateur non trouvé");
            }

            await _userRepository.DeleteAsync(id);
            response.Data = _mapper.Map<UserDto>(user);

            return HttpManager.CreateSuccessResponse(response.Data);
        }
    }
}