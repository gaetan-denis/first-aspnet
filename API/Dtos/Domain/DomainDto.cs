using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Domain
{
    public class DomainDto
    {

        [Required]
        public required string Name {get;set;}
    }
}