
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
                response.Success=false;
                response.ErrorType= EErrorType.NOTFOUND;
                return response;
            }

            response.Data = new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };
            return response;
        }

        // récupérer tous les utilisateurs

        public async  Task<ServiceResponse<IEnumerable<UserDto>>> GetAllAsync()
        {
            var response = new ServiceResponse<IEnumerable<UserDto>>();

            var users= await _userRepository.GetAllAsync();
            response.Data = users.Select(u => new UserDto
            {
                Username = u.Username,
                Email = u.Email,
                IsAdmin = u.IsAdmin
            }).ToList();

            return response;
        }

        //Ajouter un utilisateur

        public async Task<ServiceResponse<UserDto>> AddAsync(AddUserDto newUser)
        {
            var response = new ServiceResponse <UserDto>();

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
                Email= addedUser.Email,
                IsAdmin= addedUser.IsAdmin
            };

            return response;
        }

        public async Task<ServiceResponse<UserDto>> UpdateAsync(int id, UpdateUserDto updatedUser)
        {
            var response = new ServiceResponse<UserDto>();

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                response.Success = false;
                response.ErrorType = EErrorType.NOTFOUND;
                return response;
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

            return response;
        }

        public async Task<ServiceResponse< UserDto>> DeleteAsync(int id)
        {
            var response = new ServiceResponse<UserDto>();

            var user= await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                response.Success = false;
                response.ErrorType= EErrorType.NOTFOUND ;
                return response;
            }

            await _userRepository.DeleteAsync(id);
            response.Data = new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                IsAdmin= user.IsAdmin
            };
            response.ErrorType= EErrorType.SUCCESS;
            return response;
        }


    }
}