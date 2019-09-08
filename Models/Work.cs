using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academic.Models
{
    public class Work
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Required"),StringLength(255,ErrorMessage = "The length of simbols more than 255")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required"), StringLength(500, ErrorMessage = "The length of simbols more than 500")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Required"), StringLength(100, ErrorMessage = "The length of simbols more than 100")]
        public string IconField { get; set; }
    }
    
}
