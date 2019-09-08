using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academic.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public double Price { get; set; }
        [Required,StringLength(255)]
        public string Image { get; set; }
        [Required, StringLength(255)]
        public string CourseName { get; set; }
        [Required, StringLength(255)]
        public string Title { get; set; }
        [Required, StringLength(500)]
        public string Description { get; set; }
        [Required, StringLength(100)]
        public string IconField { get; set; }
    }
}
