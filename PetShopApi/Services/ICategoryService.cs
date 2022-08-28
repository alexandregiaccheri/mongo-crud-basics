namespace PetShopApi.Services
{
    public interface ICategoryService<Category>
    {
        Task<Category> CreateCategoryAsync(Category category);

        Task<Category> DeleteCategoryAsync(string id);

        Task<List<Category>> GetAllCategoriesAsync();

        //TODO: Adicionar GetCategory()

        Task<Category> UpdateCategoryAsync(string id, Category category);
    }
}
