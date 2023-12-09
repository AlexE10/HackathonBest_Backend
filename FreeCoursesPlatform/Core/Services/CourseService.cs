using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using DataLayer.Entities;
using DataLayer.Mapping;
using DataLayer.Dtos;
using DataLayer.Enums;


namespace Core.Services
{
    public class CourseService
    {
        private readonly UnitOfWork unitOfWork;

        public CourseService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCourse(AddCourseDto courseDto)
        {
            var category = await (unitOfWork.Categories.GetById(courseDto.CategoryId));
            if(category== null)
            {
                return false;
            }

            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                Creator = courseDto.Creator,
                CategoryId = courseDto.CategoryId,
                Category = category,
                Duration = courseDto.Duration,
                Difficulty = courseDto.Difficulty
            };

            await unitOfWork.Courses.InsertAsync(course);

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<CourseDto>> GetAllCourses()
        {
            var courses = await unitOfWork.Courses.GetAll();

            return courses.Select(CourseMappingExtension.ToDto).ToList();
        }

        public async Task<bool> UpdateCourse(UpdateCourseDto courseDto)
        {
            var course = await unitOfWork.Courses.GetById(courseDto.Id);
            if(course == null)
            {
                return false;
            }
            if (courseDto.Title != null)
            {
                course.Title = courseDto.Title;
            }
            if (courseDto.Description != null)
            {
                course.Description = courseDto.Description;
            }
            if (courseDto.Creator != null)
            {
                course.Creator = courseDto.Creator;
            }
            if (courseDto.CategoryId != null)
            {
                course.CategoryId = courseDto.CategoryId;
            }
            if (courseDto.Duration != null)
            {
                course.Duration = courseDto.Duration;
            }
            if (courseDto.Difficulty != null)
            {
                course.Difficulty = courseDto.Difficulty;
            }

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCourseById(int id)
        {
            var course = await unitOfWork.Courses.GetById(id);
            if(course == null)
            {
                return false;
            }

            unitOfWork.Courses.Remove(course);

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<CourseDto>> GetFilteredCourses(FilterCoursesDto filterCoursesDto)
        {
            var courses = await unitOfWork.Courses.GetAll();

            if (filterCoursesDto.CategoryId != null)
            {
                courses = courses.Where(c => c.CategoryId == filterCoursesDto.CategoryId).ToList();
            }
            if (filterCoursesDto.Duration != null)
            {
                courses = courses.Where(c => c.Duration == filterCoursesDto.Duration).ToList();
            }
            if (filterCoursesDto.Difficulty != null)
            {
                courses = courses.Where(c => c.Difficulty == (Difficulty)filterCoursesDto.Difficulty).ToList();
            }

            return courses.Select(CourseMappingExtension.ToDto).ToList();
        }   
    }
}
