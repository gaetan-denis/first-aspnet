namespace API.Dtos
{
    public class AddPostDto
    {
        [StringLength(100, MinimumLength = 3)] 
        public required string Title { get; set; }
        
        [StringLength(5000, MinimumLength = 10)]
        public required string Content { get; set; }

        [Required]
        public required int UserId { get; set; }
    }
}