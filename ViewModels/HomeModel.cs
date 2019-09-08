using Academic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academic.ViewModels
{
    public class HomeModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<RepeatTitle> RepeatTitles { get; set; }
        public IEnumerable<Work> Works { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
