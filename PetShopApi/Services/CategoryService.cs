using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Models;

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

        public async Task<Category> CreateCategoryAsync(Category category)
        {
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

        public async Task<Category> UpdateCategoryAsync(string id, Category category)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
            var result =
            await _categoryCollection.UpdateOneAsync(filter, Builders<Category>
                .Update.Set(c => c.CategoryName, category.CategoryName));
            return await _categoryCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
