namespace API.Dtos.Domain
{
    public class DomainDto
    {
        [Required]
        public required int DomainId { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}