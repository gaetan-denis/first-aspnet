namespace API.Services
{
    using API.Dtos.Responses;
    using API.Dtos.User;
    using API.Entities;

    public interface IUserService
    {
        // Récupère un user spécifique en fonction de son id.
        Task<ServiceResponse<UserDto>>GetByIdAsync(int id);
        
        Task<ServiceResponse<IEnumerable<UserDto>>>GetAllAsync();
        //Permet d'ajouter un nouvel user dans le système.
        Task <ServiceResponse<UserDto>>AddAsync(UserDto user);
        //Permet de modifier un user dans le système.
        Task <ServiceResponse<UserDto>>UpdateAsync(UserDto user);
        // Supprime un user dans le système.
        Task <ServiceResponse<UserDto>>DeleteAsync(int id);
    }
}