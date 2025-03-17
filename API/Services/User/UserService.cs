
using API.Controllers;
using API.Dtos.User;
using API.Enums;
using API.Repositories;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Récupérer un utilisateur par ID

        public async Task<ServiceResponse<UserDto>> GetByIdAsync(int id)
        {
            var response = new ServiceResponse<UserDto>();
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return HttpManager.CreateErrorResponse<UserDto>(EErrorType.NOTFOUND, "utilisateur non trouvé");
            }

            response.Data = new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
             return HttpManager.CreateSuccessResponse(response.Data);
        }

        // récupérer tous les utilisateurs

        public async Task<ServiceResponse<IEnumerable<UserDto>>> GetAllAsync()
        {
            var response = new ServiceResponse<IEnumerable<UserDto>>();

            var users = await _userRepository.GetAllAsync();

            if (users == null)
            {
                return HttpManager.CreateErrorResponse<IEnumerable<UserDto>>(EErrorType.NOTFOUND, "utilisateurs non trouvés");
            }
            response.Data = users.Select(u => new UserDto
            {
                Username = u.Username,
                Email = u.Email,
                IsAdmin = u.IsAdmin
            }).ToList();

            return HttpManager.CreateSuccessResponse(response.Data);
        }

        //Ajouter un utilisateur

        public async Task<ServiceResponse<UserDto>> AddAsync(AddUserDto newUser)
        {
            var response = new ServiceResponse<UserDto>();

            var user = new User
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password,
                IsAdmin = newUser.IsAdmin
            };

            var addedUser = await _userRepository.AddAsync(user);

            response.Data = new UserDto
            {
                Username = addedUser.Username,
                Email = addedUser.Email,
                IsAdmin = addedUser.IsAdmin
            };

             return HttpManager.CreateSuccessResponse(response.Data);
        }

        public async Task<ServiceResponse<UserDto>> UpdateAsync(int id, UpdateUserDto updatedUser)
        {
            var response = new ServiceResponse<UserDto>();

            
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                
                return HttpManager.CreateErrorResponse<UserDto>(EErrorType.NOTFOUND, "utilisateur non trouvé");
            }

            
            existingUser.Username = updatedUser.Username;
            existingUser.Email = updatedUser.Email;
            

            
            var updated = await _userRepository.UpdateAsync(existingUser);

            
            response.Data = new UserDto
            {
                Username = updated.Username,
                Email = updated.Email,
                IsAdmin = updated.IsAdmin  
            };

            
             return HttpManager.CreateSuccessResponse(response.Data);
        }

        public async Task<ServiceResponse<UserDto>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<UserDto>();

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
               return HttpManager.CreateErrorResponse<UserDto>(EErrorType.NOTFOUND, "utilisateur non trouvé");
            }

            await _userRepository.DeleteAsync(id);
            response.Data = new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };

            return HttpManager.CreateSuccessResponse(response.Data);
        }


    }
}