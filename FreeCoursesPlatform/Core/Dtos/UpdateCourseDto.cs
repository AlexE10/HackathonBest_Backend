using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.Enums;

namespace Core.Dtos
{
    public class UpdateCourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Creator { get; set; }
        public int CategoryId { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
