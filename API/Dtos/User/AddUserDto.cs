namespace API.Dtos.User
{
    public class AddUserDto
    {
        [MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }

        // Initialisation de la valeur par défaut à false
        public bool IsAdmin { get; set; } = false;

    }
}