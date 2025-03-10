using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class PostDomain
    {   
        [Key]
        [Column("POSTDOM_postId")]
        public int PostId {get;set;}
        
        
        public required Post Post {get;set;}

        [Key]
        public int DomainId {get;set;}
        public required Domain Domain {get;set;}
    }
}