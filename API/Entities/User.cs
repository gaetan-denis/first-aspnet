using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class User
    {
        // dépendance ES email, mail etc, 

        [Column("USER_Id")]
        public int Id { get; set; }
        [Column("USER_Username")]
        [MaxLength(50)]
        public required string Username { get; set; }
        [Column("USER_Email")]
        [EmailAddress]
        public required string Email {get;set;}
        
        [Column("USER_Password")]
        [Required]
        public required string Password { get; set; }
        [Column("USER_IsAdmin")]
        public bool IsAdmin { get; set; }
        [Column("USER_CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [Column("USER_UpdatedAt")]
        public DateTime UpdatedAt { get; set; }

        // Définit la relations avec les posts (un utilisateur peut avoir plusieurs posts)
        public ICollection<Post> Posts { get; set; } = new List<Post>(); // Liste des posts associés
    }
}

