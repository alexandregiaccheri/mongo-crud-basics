using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Models;
using PetShopApi.Models.DTO;

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

        public async Task<Order> CreateOrderAsync(CreateOrderDTO dto)
        {
            var order = new Order()
            {
                CreationDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                OrderItems = dto.OrderItems,
                OrderStatus = dto.OrderStatus,
                PaymentStatus = dto.PaymentStatus,
                ShippingAddress = dto.ShippingAddress,
                UserId = dto.UserId
            };

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

        public async Task<Order> UpdateOrderAsync(string id, Order order, UpdateOrderDTO dto)
        {
            var filter = Builders<Order>.Filter.Eq(p => p.Id, id);
            var changesWereMade = false;

            if (!string.IsNullOrEmpty(dto.OrderStatus)
                && dto.OrderStatus != order.OrderStatus)
            {
                await _orderCollection.UpdateOneAsync(filter, Builders<Order>.Update
                    .Set(o => o.OrderStatus, dto.OrderStatus));

                changesWereMade = true;
            }

            if (!string.IsNullOrEmpty(dto.PaymentStatus)
                && dto.PaymentStatus != order.OrderStatus)
            {
                await _orderCollection.UpdateOneAsync(filter, Builders<Order>.Update
                    .Set(o => o.PaymentStatus, dto.PaymentStatus));

                changesWereMade = true;
            }

            if (changesWereMade)
                await _orderCollection.UpdateOneAsync(filter, Builders<Order>.Update
                    .Set(o => o.LastUpdated, DateTime.Now));

            return await _orderCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
