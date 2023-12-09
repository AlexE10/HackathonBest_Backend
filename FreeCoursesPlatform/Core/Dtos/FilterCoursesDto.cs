using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class FilterCoursesDto
    {
        public int CategoryId { get; set; }
        public string Duration { get; set; }
        public int Difficulty { get; set; }
    }
}
