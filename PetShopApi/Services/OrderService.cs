using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Models;

namespace PetShopApi.Services
{
    public class OrderService : IOrderService<Order>
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _orderCollection = mongoDatabase.GetCollection<Order>(mongoDbSettings.Value.OrderCollection);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.CreationDate = DateTime.Now;
            order.LastUpdated = DateTime.Now;
            await _orderCollection.InsertOneAsync(order);
            return order;
        }

        public async Task<Order> DeleteOrderAsync(string id)
        {
            var filter = Builders<Order>.Filter.Eq(p => p.Id, id);
            var order = await _orderCollection.Find(filter).SingleAsync();
            await _orderCollection.DeleteOneAsync(filter);
            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Order> GetOrderAsync(string id)
        {
            var filter = Builders<Order>.Filter.Eq(p => p.Id, id);
            return await _orderCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<Order> UpdateOrderAsync(string id, Order order)
        {
            var filter = Builders<Order>.Filter.Eq(p => p.Id, id);
            var result =
            await _orderCollection.UpdateOneAsync(filter, Builders<Order>.Update
                .Set(o => o.LastUpdated, DateTime.Now)
                .Set(o => o.OrderItems, order.OrderItems)
                .Set(o => o.OrderStatus, order.OrderStatus)
                .Set(o => o.PaymentStatus, order.PaymentStatus)
                .Set(o => o.ShippingAddress, order.ShippingAddress)
                .Set(o => o.UserId, order.UserId));
            return await _orderCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
