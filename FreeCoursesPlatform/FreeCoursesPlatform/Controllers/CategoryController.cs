using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCoursesPlatform.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("all-categories")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }
    }
}
