using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Core.Dtos;
using System.Security.Claims;

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
        [Authorize(Roles = "Creator")]
        public async Task<IActionResult> AddCourse([FromBody] AddCourseDto courseData)
        {
            // Extract the user ID from the token claims
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            int creatorId;
            if (!int.TryParse(userIdClaim.Value, out creatorId))
            {
                return Unauthorized("Invalid User ID in token");
            }

            if (await _courseService.AddCourse(courseData, creatorId))
            {
                return Ok("Course added successfully");
            }
            else
            {
                return BadRequest("Something wrong happened");
            }
        }

        [HttpPut("update-course")]
        [Authorize(Roles = "Creator")]
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
        [Authorize(Roles = "Creator")]
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

        [HttpPost("enroll-to-course")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> EnrollToCourse([FromBody] EnrollToCourseDto enrollData)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Invalid User ID in token");
            }
            enrollData.UserId = userId;
            if (await _courseService.EnrollToCourse(enrollData))
            {
                return Ok("Enrolled successfully");
            }
            else
            {
                return BadRequest("Something wrong happened");
            }
        }
        [HttpGet("get-enrolled-courses-by-userId")]
        [Authorize(Roles = "Reader")]
        public async Task<ActionResult<List<Course>>> GetEnrolledCoursesById()
        {
            // Extract the user ID from the token claims
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Invalid User ID in token");
            }

            var courses = await _courseService.GetEnrolledCoursesById(userId);
            return Ok(courses);
        }

        [HttpGet("get-created-by-userId")]
        [Authorize(Roles = "Creator")]
        public async Task<ActionResult<List<Course>>> GetCreatedCoursesById()
        {
            // Extract the user ID from the token claims
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized("Invalid User ID in token");
            }

            var courses = await _courseService.GetCreatedCoursesById(userId);
            return Ok(courses);
        }
    }
}
