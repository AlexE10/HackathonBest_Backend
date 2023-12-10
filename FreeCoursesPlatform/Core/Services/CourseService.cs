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
using Infrastructure.Exceptions;


namespace Core.Services
{
    public class CourseService
    {
        private readonly UnitOfWork unitOfWork;

        public CourseService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCourse(AddCourseDto courseDto, int creatorId)
        {
            var category = await (unitOfWork.Categories.GetById(courseDto.CategoryId));
            var creator = await unitOfWork.Users.GetByIdWithCoursesAsync(creatorId);
            if (category == null || creator == null)
            {
                return false;
            }

            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                Creator = creator,
                CategoryId = courseDto.CategoryId,
                Category = category,
                Duration = courseDto.Duration,
                Difficulty = courseDto.Difficulty
            };

            creator.CreatedCourses.Add(course);
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
            var creator = await unitOfWork.Users.GetById(courseDto.CreatorId);
            if (course == null || creator == null)
            {
                return false;
            }
            course.Creator = creator;
            if (courseDto.Title != null)
            {
                course.Title = courseDto.Title;
            }
            if (courseDto.Description != null)
            {
                course.Description = courseDto.Description;
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

        public async Task<bool> EnrollToCourse(EnrollToCourseDto enrollToCourseDto)
        {
            var user = await unitOfWork.Users.GetByIdWithEnrolledCourses(enrollToCourseDto.UserId);
            if(user == null)
            {
                return false;
            }

            var course = await unitOfWork.Courses.GetByIdWithUsersAsync(enrollToCourseDto.CourseId);
            if(course == null)
            {
                return false;
            }

            user.EnrolledCourses.Add(course);
            course.Users.Add(user);

            await unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<List<CourseDto>> GetEnrolledCoursesById(int userId)
        {
            var user = await unitOfWork.Users.GetById(userId);
            if(user == null)
            {
                throw new ForbiddenException("User not found");
            }

            var courses = await unitOfWork.Courses.GetAllWithUsersAsync();
            courses = courses.Where(c => c.Users.Contains(user)).ToList();
            return courses.Select(CourseMappingExtension.ToDto).ToList();
        }

        public async Task<List<CourseDto>> GetCreatedCoursesById(int userId)
        {
            var user = await unitOfWork.Users.GetById(userId);
            if (user == null)
            {
                throw new ForbiddenException("User not found");
            }

            var courses = await unitOfWork.Courses.GetAll();
            courses = courses.Where(c => c.CreatorId == userId).ToList();
            return courses.Select(CourseMappingExtension.ToDto).ToList();
        }

    }
}
