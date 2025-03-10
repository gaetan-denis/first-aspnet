using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public class Post {
        [Column("Post_id")]
        public int Id {get;set;}
        [Column("POST_userId")]
        public int UserId {get;set;}
        [Column("POST_title")]
        public  required string Title {get;set;}
        [Column("POST_content")]
        public  required string Content {get;set;}
        [Column("POST_createddAt")]
        public DateTime CreatedAt {get;set;}
        [Column("POST_updated1t")]
        public DateTime UpdateAt {get;set;}
        
    }
}