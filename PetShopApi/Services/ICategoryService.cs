namespace PetShopApi.Services
{
    public interface ICategoryService<Category>
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<Category> DeleteCategoryAsync(string id);

        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryAsync(string id);

        Task<Category> UpdateCategoryAsync(string id, Category category);
    }
}
