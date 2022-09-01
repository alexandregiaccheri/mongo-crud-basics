using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
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

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            return Ok(await _productService.CreateProductAsync(product));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var result = await _productService.GetProductAsync(id);
            if (result == null)
                return NotFound();
            return Ok(await _productService.DeleteProductAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var result = await _productService.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var result = await _productService.GetProductAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(string id, [FromBody] Product product)
        {
            var result = await _productService.GetProductAsync(id);
            if (result == null)
                return NotFound();
            return await _productService.UpdateProductAsync(id, product);
        }
    }
}
