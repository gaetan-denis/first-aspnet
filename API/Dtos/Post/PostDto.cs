using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PostDto
    {

        [StringLength(100, MinimumLength = 3)]
        public required string Title { get; set; }

        [StringLength(5000, MinimumLength = 10)]
        public required string Content { get; set; }

        [Required]
        public required int UserId { get; set; }
    }
}