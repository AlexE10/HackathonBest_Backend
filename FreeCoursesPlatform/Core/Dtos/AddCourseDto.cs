using DataLayer.Entities;
using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class AddCourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public int CategoryId { get; set; }
        public string Duration { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
