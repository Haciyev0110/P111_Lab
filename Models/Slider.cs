using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Academic.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Required"),StringLength(255,ErrorMessage ="Length not more than 255 simbol")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required"), StringLength(255, ErrorMessage = "Length not more than 255 simbol")]
        public string Image { get; set; }
        [NotMapped]
        [Required(ErrorMessage ="Required")]
        public IFormFile Photo { get; set; }
        [NotMapped]
        public IFormFile PhotoUpdate { get; set; }
    }
}
