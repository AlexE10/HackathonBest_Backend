using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Core.Dtos;

namespace FreeCoursesPlatform.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("all-courses")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            var courses = await _courseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpPost("add-course")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseDto courseData)
        {
            if (await _courseService.AddCourse(courseData))
            {
                return Ok("Course added successfully");
            }
            else
            {
                return BadRequest("Something wrong happened");
            }
        }

        [HttpPut("update-course")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto courseData)
        {
            if (await _courseService.UpdateCourse(courseData))
            {
                return Ok("Course updated successfully");
            }
            else
            {
                return BadRequest("Something wrong happened");
            }
        }

        [HttpDelete("delete-course")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCourse([FromBody] int courseId)
        {
            if (await _courseService.DeleteCourseById(courseId))
            {
                return Ok("Course deleted successfully");
            }
            else
            {
                return BadRequest("Something wrong happened");
            }
        }

        [HttpPost("get-filtered")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Course>>> GetFilteredCourses([FromBody] FilterCoursesDto filterData)
        {
            var courses = await _courseService.GetFilteredCourses(filterData);
            return Ok(courses);
        }
    }
}
