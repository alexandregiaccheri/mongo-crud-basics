using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
using PetShopApi.Models.DTO;
using PetShopApi.Services;

namespace PetShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Creates a new product category.
        /// </summary>
        /// <remarks>
        /// Creates the category and automatically generates a slug based on the category name.
        /// Creation date and last updated are also automatically resolved.
        /// </remarks>
        /// <param name="dto">Payload consisting of only one field, category name, which is required.</param>
        /// <returns>The newly created Category entry.</returns>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CategoryDTO dto)
        {
            return Ok(await _categoryService.CreateCategoryAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(string id)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            if (result == null)
                return NotFound();
            return Ok(await _categoryService.DeleteCategoryAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(string id)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Category>> UpdateCategory(string id, [FromBody] CategoryDTO dto)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
                return NotFound();
            return await _categoryService.UpdateCategoryAsync(id, category, dto);
        }
    }
}
