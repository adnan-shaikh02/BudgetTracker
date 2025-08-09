using BudgetTracker.Api.Models;
using BudgetTracker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Api.Controllers
{
    [Route("api/v{version:apiVersion}/Category")]
    [ApiController]
    [ApiVersion("1.0")]

    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        [Route("GetAllCategories")]
        [ProducesResponseType(typeof(Category) ,200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Category), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            var result = await _categoryService.AddCategoryAsync(category);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] Category category)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(category);
            return Ok(updatedCategory);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [Route("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (result)
            {
                return Ok("Category deleted successfully.");
            }
            return NotFound("Category not found.");
        }
    }
}
