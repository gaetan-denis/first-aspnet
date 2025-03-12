using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.User
{
    public class UpdateUserDto
    {
        public required string Username { get; set; }
        
        [EmailAddress]
        public required string Email {get;set;}
       
        public required string Password { get; set; }
      
        public bool IsAdmin { get; set; }
               
        public DateTime CreatedAt { get; set; }
       
        public DateTime UpdatedAt { get; set; }  
    }
}