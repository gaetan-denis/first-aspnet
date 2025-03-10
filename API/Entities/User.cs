using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;





namespace API.Entities
{
    public class User
    {
        [Column("USER_Id")]
        public int Id {get;set;}
        
        [MaxLength(50)]
        [Required]
        [Column("USER_Username")]
        public required string Username {get;set;}

        [EmailAddress]
        [Column("USER_email")]
        public required string Email {get;set;}
        
        [Required]
        [Column("USER_Password")]
        public required string Password {get;set;}

        [Column("USER_IsAdmin")]
        public bool IsAdmin {get;set;}

        [Required]
        [Column("USER_CreatedAt")]
        public DateTime CreatedAt {get;set;}
        
        [Column("USER_UpdatedAt")]
        public DateTime UpdatedAt {get;set;}

        // Définit la relations avec les posts (un utilisateur peut avoir plusieurs posts)
        public ICollection<Post> Posts { get; set; } = new List<Post>(); // Liste des posts associés
    }
}

