using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AddPostDto
    {
        public  required string Title {get;set;}
        
        public required string Content {get;set;}
        
        public DateTime CreatedAt {get;set;} // Utile ou on le définit dans la logique métier?
    
        public DateTime UpdateAt {get;set;}

        public required int UserId {get;set;}
    }
}