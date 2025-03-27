namespace API.Dtos.Post
{
    public class UpdatePostDto
    {
        [StringLength(100, MinimumLength = 3)]
        public required string Title { get; set; }

        [StringLength(5000, MinimumLength = 10)]
        public required string Content { get; set; }

        public int UserId { get; set; }
    }
}