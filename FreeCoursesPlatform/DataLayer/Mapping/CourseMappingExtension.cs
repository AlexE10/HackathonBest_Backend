using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mapping
{
    public static class CourseMappingExtension
    {
        public static CourseDto ToDto(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Creator = course.Creator,
                CategoryId = course.CategoryId,
                Duration = course.Duration,
                Difficulty = course.Difficulty,
            };
        }

        public static List<CourseDto> ToCourseDtos(this List<Course> courses)
        {
            return courses.Select(ToDto).ToList();
        }
    }
}
