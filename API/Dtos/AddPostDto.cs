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
        
        public DateTime CreatedAt {get;set;}
    
        public DateTime UpdateAt {get;set;}
    }
}