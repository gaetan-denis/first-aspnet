namespace API.Services
{
    using API.Dtos;
    using API.Dtos.Responses;
    using API.Dtos.User;
    using API.Entities;

    public interface IUserService
    {
        // Récupère un user spécifique en fonction de son id.
        Task<ServiceResponse<UserDto>> GetByIdAsync(int id);

        Task<ServiceResponse<Pagination<UserDto>>> GetAllAsync(int page, int window);
        //Permet d'ajouter un nouvel user dans le système.
        Task<ServiceResponse<UserDto>> AddAsync(AddUserDto newUser);
        //Permet de modifier un user dans le système.y
        Task<ServiceResponse<UserDto>> UpdateAsync(int id, UpdateUserDto updatedUser);
        // Supprime un user dans le système.
        Task<ServiceResponse<UserDto>> DeleteAsync(int id);
    }
}