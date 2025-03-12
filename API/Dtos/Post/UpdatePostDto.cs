using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Post
{
    public class UpdatePostDto
    {
        public required string Content {get;set;}
        public DateTime UpdateAt {get;set;}
    }
}