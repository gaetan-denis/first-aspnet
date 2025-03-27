namespace API.Entities
{
    public class PostDomain
    {   
        
        [Column("POSTDOM_postId")]
        public int PostId {get;set;}
        
        
        public required Post Post {get;set;}

        
        public int DomainId {get;set;}
        public required Domain Domain {get;set;}
    }
}