using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Helper;
using PetShopApi.Models;
using PetShopApi.Models.DTO;

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

        public async Task<Product> CreateProductAsync(CreateProductDTO dto)
        {
            var product = new Product()
            {
                CategoryId = dto.CategoryId!,
                CreationDate = DateTime.Now,
                ImgUrl = dto.ImgUrl!,
                LastUpdated = DateTime.Now,
                ProductDescription = dto.ProductDescription!,
                ProductManufacturer = dto.ProductManufacturer!,
                ProductPrice = (int)dto.ProductPrice!,
                ProductTitle = dto.ProductTitle!,
                Slug = SlugHelper.RemoveAccent
                      (SlugHelper.GenerateSlug(dto.ProductTitle!)),
                Stock = (int)dto.Stock!
            };

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

        public async Task<Product> UpdateProductAsync(string id, Product product, UpdateProductDTO dto)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var changesWereMade = false;

            if (!string.IsNullOrEmpty(dto.CategoryId)
                && dto.CategoryId != product.CategoryId)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.CategoryId, dto.CategoryId));

                changesWereMade = true;
            }

            if (!string.IsNullOrEmpty(dto.ImgUrl)
                && dto.ImgUrl != product.ImgUrl)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.ImgUrl, dto.ImgUrl));

                changesWereMade = true;
            }

            if (!string.IsNullOrEmpty(dto.ProductDescription)
                && dto.ProductDescription != product.ProductDescription)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.ProductDescription, dto.ProductDescription));

                changesWereMade = true;
            }

            if (!string.IsNullOrEmpty(dto.ProductManufacturer)
                && dto.ProductManufacturer != product.ProductManufacturer)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.ProductManufacturer, dto.ProductManufacturer));

                changesWereMade = true;
            }

            if (dto.ProductPrice != null && dto.ProductPrice > 0
                && dto.ProductPrice != product.ProductPrice)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.ProductPrice, dto.ProductPrice));

                changesWereMade = true;
            }

            if (!string.IsNullOrEmpty(dto.ProductTitle)
                && dto.ProductTitle != product.ProductTitle)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.ProductTitle, dto.ProductTitle)
                .Set(p => p.Slug, SlugHelper.RemoveAccent(
                                  SlugHelper.GenerateSlug(dto.ProductTitle))));

                changesWereMade = true;
            }

            if (dto.Stock != null && dto.Stock >= 0
                && dto.Stock != product.Stock)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.Stock, dto.Stock));

                changesWereMade = true;
            }

            if (changesWereMade)
            {
                await _productCollection.UpdateOneAsync(filter, Builders<Product>.Update
                .Set(p => p.LastUpdated, DateTime.Now));
            }

            return await _productCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
