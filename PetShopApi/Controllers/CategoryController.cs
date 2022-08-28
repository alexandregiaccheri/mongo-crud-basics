using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
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

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            return Ok(await _categoryService.CreateCategoryAsync(category));
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
        public async Task<ActionResult<Category>> UpdateCategory(string id, [FromBody] Category category)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            if (result == null)
                return NotFound();
            return await _categoryService.UpdateCategoryAsync(id, category);
        }
    }
}
