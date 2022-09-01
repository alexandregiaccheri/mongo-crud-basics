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
        /// Creates the category and automatically generates a slug based on the category name. <br/>
        /// Creation date and last updated are also automatically resolved. <br/>
        /// Returns the newly created category entry.
        /// </remarks>
        /// <param name="dto">Payload consisting of only one field, category name, which is required.</param>       
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CategoryDTO dto)
        {
            return Ok(await _categoryService.CreateCategoryAsync(dto));
        }

        /// <summary>
        /// Deletes an existing product category.
        /// </summary>
        /// <remarks>
        /// Deletes a category based on it's id.
        /// Returns the deleted category entry.
        /// </remarks>
        /// <param name="id">The id of an existing category. Must be a valid 24 digits hex value.</param>        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(string id)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            if (result == null)
                return NotFound();
            return Ok(await _categoryService.DeleteCategoryAsync(id));
        }

        /// <summary>
        /// Retrieves a list with all categories.
        /// </summary>
        /// <remarks>
        /// Returns the list with all categories.
        /// </remarks>        
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategoriesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a category based on it's id.
        /// </summary>
        /// <remarks>
        /// Returns the category if it exists.
        /// </remarks>
        /// <param name="id">The id of an existing category. Must be a valid 24 digits hex value.</param>        
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(string id)
        {
            var result = await _categoryService.GetCategoryAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Finds and updates a category with a new name.
        /// </summary>
        /// <remarks>
        /// Returns the updated category or the original category if no changes were made. <br/>
        /// Changes will only be commited if the payload contains a new (different) name for the category, that is not null, empty or "string". <br/>
        /// If the payload passes validation, a new slug will be generated to reflect changes.
        /// </remarks>
        /// <param name="id">The id of an existing category. Must be a valid 24 digits hex value.</param>
        /// <param name="dto">Payload with the new name for the existing category. <br/></param>       
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
