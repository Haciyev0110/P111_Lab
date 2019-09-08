using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academic.Models
{
    public class User:IdentityUser
    {
        [Required]
        [StringLength(maximumLength: 150, MinimumLength = 5, ErrorMessage = "Dont Correct")]
        public string Fullname { get; set; }
    }
}
