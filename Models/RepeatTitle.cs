using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Academic.Models
{
    public class RepeatTitle
    {
        public int Id { get; set; }
        [Required,StringLength(255)]
        public string Title { get; set; }
    }
}
