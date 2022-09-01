using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Models;

namespace PetShopApi.Services
{
    public class ProductService : IProductService<Product>
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _productCollection = mongoDatabase.GetCollection<Product>(mongoDbSettings.Value.ProductCollection);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.CreationDate = DateTime.Now;
            product.LastUpdated = DateTime.Now;
            await _productCollection.InsertOneAsync(product);
            return product;
        }

        public async Task<Product> DeleteProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var product = await _productCollection.Find(filter).SingleAsync();
            await _productCollection.DeleteOneAsync(filter);
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Product> GetProductAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await _productCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Product> UpdateProductAsync(string id, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var result =
            await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.ImgUrl, product.ImgUrl)
                .Set(p => p.LastUpdated, DateTime.Now)
                .Set(p => p.ProductDescription, product.ProductDescription)
                .Set(p => p.ProductManufacturer, product.ProductManufacturer)
                .Set(p => p.ProductPrice, product.ProductPrice)
                .Set(p => p.ProductTitle, product.ProductTitle)
                .Set(p => p.Stock, product.Stock));
            return await _productCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
