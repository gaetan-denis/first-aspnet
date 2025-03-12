using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Post 
    {
        [Key]
        [Required]
        [Column("POST_id")]
        public int Id {get;set;}

        [Required]
        [Column("POST_userId")]

        public int UserId {get;set;}

        [Required]
    
        [StringLength(100, MinimumLength = 3)] 
        [Column("POST_title")]
        public  required string Title {get;set;}
        
        [Required]
        [StringLength(5000, MinimumLength = 10)]
        [Column("POST_content")]
        public required string Content {get;set;}
        
        [Required]
        [Column("POST_createddAt")]
        public DateTime CreatedAt {get;set;}
        
        [Column("POST_updatedAt")]
        public DateTime UpdateAt {get;set;}
        
        //Propriété de navigation propre à Entity Framework
        public required User User {get;set;}

        // Définit la relation avec les domaines (Un post peut avoir plusieurs domaines).
        public ICollection<PostDomain>PostDomains {get;set;} = new List<PostDomain>(); //Liste des domaines associés
    }
}