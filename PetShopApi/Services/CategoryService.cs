using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Helper;
using PetShopApi.Models;
using PetShopApi.Models.DTO;

namespace PetShopApi.Services
{
    public class CategoryService : ICategoryService<Category>
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoryService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _categoryCollection = mongoDatabase.GetCollection<Category>(mongoDbSettings.Value.CategoryCollection);
        }

        public async Task<Category> CreateCategoryAsync(CategoryDTO dto)
        {
            var category = new Category()
            {
                CategoryName = dto.CategoryName,
                CreationDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                Slug = SlugHelper.RemoveAccent(SlugHelper.GenerateSlug(dto.CategoryName))
            };                        
            await _categoryCollection.InsertOneAsync(category);
            return category;
        }

        public async Task<Category> DeleteCategoryAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
            var category = await _categoryCollection.Find(filter).SingleAsync();
            await _categoryCollection.DeleteOneAsync(filter);
            return category;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
            return await _categoryCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Category> UpdateCategoryAsync(string id, Category category, CategoryDTO dto)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id);

            if (dto.CategoryName != null &&
                dto.CategoryName != "" &&
                dto.CategoryName != "string" &&
                dto.CategoryName != category.CategoryName)
                await _categoryCollection.UpdateOneAsync(filter, Builders<Category>
                    .Update.Set(c => c.CategoryName, dto.CategoryName)
                           .Set(c => c.LastUpdated, DateTime.Now)
                           .Set(c => c.Slug, SlugHelper.RemoveAccent(
                                             SlugHelper.GenerateSlug(dto.CategoryName))));

            return await _categoryCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
