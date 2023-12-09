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
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all-users")]
        [Authorize(Roles = "Creator")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerData)
        {
            if (await _userService.RegisterAsync(registerData))
            {
                return Ok("User registered successfully");
            }
            else
            {
                return BadRequest("Something wrong happened");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto loginData)
        {
            var jwtToken = await _userService.ValidateLoginAsync(loginData);
            return Ok(new { token = jwtToken });
        }
    }
}
