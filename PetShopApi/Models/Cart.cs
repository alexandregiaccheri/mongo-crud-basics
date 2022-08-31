using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetShopApi.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string ProductId { get; set; } = null!;        

        [BsonRequired]
        public int ProductQuantity { get; set; }        
    }
}
