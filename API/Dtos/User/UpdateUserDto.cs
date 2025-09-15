namespace API.Dtos.User
{
    public class UpdateUserDto
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public required string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}