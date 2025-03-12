using System.ComponentModel.DataAnnotations;


namespace API.Dtos.User
{
    public class UserDto
    {
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        public bool IsAdmin { get; set; }
    }
}