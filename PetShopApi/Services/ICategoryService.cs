using PetShopApi.Models.DTO;

namespace PetShopApi.Services
{
    public interface ICategoryService<Category>
    {
        Task<Category> CreateCategoryAsync(CategoryDTO dto);

        Task<Category> DeleteCategoryAsync(string id);

        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryAsync(string id);

        Task<Category> UpdateCategoryAsync(string id, Category category, CategoryDTO dto);
    }
}
