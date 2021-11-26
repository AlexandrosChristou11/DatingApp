using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    
    public class RegisterDto
    {
        [Required]
        public string userName { get; set; }

        [Required]
        // [RegularExpression] - For validation - set up type (e.g Phone, email etc..)
        public string Password { get; set; }

    }
}