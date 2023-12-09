using DataLayer.Entities;
using DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Dtos
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
        public string Duration { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
