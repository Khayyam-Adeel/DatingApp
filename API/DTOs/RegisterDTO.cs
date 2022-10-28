using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RegisterDTO
    {  [Required]
       public string Username{get;set;}
       [Required]
       [StringLength(9,MinimumLength =4)]
        public string password{get;set;}
    }
}