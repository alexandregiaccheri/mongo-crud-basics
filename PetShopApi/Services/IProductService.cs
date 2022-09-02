using PetShopApi.Models.DTO;

namespace PetShopApi.Services
{
    public interface IProductService<Product>
    {
        Task<Product> CreateProductAsync(ProductDTO dto);

        Task<Product> DeleteProductAsync(string id);

        Task<List<Product>> GetAllProductsAsync();

        Task<Product> GetProductAsync(string id);

        Task<Product> UpdateProductAsync(string id, Product product, ProductDTO dto);
    }
}
