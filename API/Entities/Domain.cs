namespace API.Entities
{
    public class Domain
    {
        [Key]
        [Required]
        [Column("DOMA_id")]
        public int Id { get; set; }

        [Required]
        [Column("DOMA_name")]
        public required string Name { get; set; }

        // Relation Many-to-Many entre Domain et Post via PostDomain
        public ICollection<PostDomain> PostDomains { get; set; } = new List<PostDomain>();
    }
}