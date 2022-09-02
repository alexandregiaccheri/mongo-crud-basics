using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
using PetShopApi.Models.DTO;
using PetShopApi.Services;

namespace PetShopApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <remarks>
        /// Creates the product and automatically generates a slug based on the product name/title. <br/>
        /// Creation date and last updated are also automatically resolved. <br/>
        /// Returns the newly created product entry.
        /// </remarks>
        /// <param name="dto">Payload consisting of several fields, all are required to create a new product.</param>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductDTO dto)
        {
            if (string.IsNullOrEmpty(dto.CategoryId) || dto.CategoryId == "string" ||
                string.IsNullOrEmpty(dto.ImgUrl) || dto.ImgUrl == "string" ||
                string.IsNullOrEmpty(dto.ProductDescription) || dto.ProductDescription == "string" ||
                string.IsNullOrEmpty(dto.ProductManufacturer) || dto.ProductManufacturer == "string" ||
                dto.ProductPrice == null || dto.ProductPrice < 1 ||
                string.IsNullOrEmpty(dto.ProductTitle) || dto.ProductTitle == "string" ||
                dto.Stock == null || dto.Stock < 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(await _productService.CreateProductAsync(dto));
            }
        }

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <remarks>
        /// Deletes a product based on it's id.
        /// Returns the deleted product entry.
        /// </remarks>
        /// <param name="id">The id of an existing product. Must be a valid 24 digits hex value.</param>        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var result = await _productService.GetProductAsync(id);
            if (result == null)
                return NotFound();
            return Ok(await _productService.DeleteProductAsync(id));
        }

        /// <summary>
        /// Retrieves a list with all products.
        /// </summary>
        /// <remarks>
        /// Returns the list with all products.
        /// </remarks>    
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a product based on it's id.
        /// </summary>
        /// <remarks>
        /// Returns the product if it exists.
        /// </remarks>
        /// <param name="id">The id of an existing product. Must be a valid 24 digits hex value.</param>        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var result = await _productService.GetProductAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Finds and updates a product with new info.
        /// </summary>
        /// <remarks>
        /// Returns the updated product or the original product if no changes were made. <br/>
        /// Changes will only be commited if the payload contains a new (different) value for at least one property, and it must not be null, empty or "string". <br/>
        /// Values lower than 1 for product price and lower than 0 for stock won't be accepted. <br/>
        /// If the payload passes validation, a new slug will be generated to reflect changes.
        /// </remarks>
        /// <param name="id">The id of an existing product. Must be a valid 24 digits hex value.</param>
        /// <param name="dto">Payload with the new values for the existing product. <br/></param>       
        [HttpPatch("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(string id, [FromBody] ProductDTO dto)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
                return NotFound();
            return await _productService.UpdateProductAsync(id, product, dto);
        }
    }
}
