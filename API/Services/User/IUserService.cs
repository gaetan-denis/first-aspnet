namespace API.Services
{
    using API.Dtos.Responses;
    using API.Dtos.User;
    using API.Entities;

    public interface IUserService
    {
        // Retourne une tâche contenant un objet User.
        Task<ServiceResponse<UserDto>> GetByIdAsync(int id);
        // Récupère un user spécifique en fonction de son id.
        Task<ServiceResponse<IEnumerable<UserDto>>> GetAllAsync();
        //Permet d'ajouter un nouvel user dans le système.
        Task <ServiceResponse<UserDto>>AddAsync(User user);
        //Permet de modifier un user dans le système.
        Task <ServiceResponse<UserDto>>UpdateAsync(User user);
        // Supprime un user dans le système.
        Task <ServiceResponse<UserDto>>DeleteAsync(int id);
    }
}