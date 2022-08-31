using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetShopApi.Models
{
    public class Order
    {
        [BsonRequired]
        public DateTime CreationDate { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public DateTime LastUpdated { get; set; }

        [BsonRequired]
        public List<Cart> OrderItems { get; set; } = null!;

        [BsonRequired]
        public string OrderStatus { get; set; } = null!;

        [BsonRequired]
        public string PaymentStatus { get; set; } = null!;
        
        [BsonRequired]
        public Address ShippingAddress { get; set; } = null!;

        [BsonRequired]
        public string UserId { get; set; } = null!;
    }
}
