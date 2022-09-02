using MongoDB.Bson.Serialization.Attributes;

namespace PetShopApi.Models
{
    public class Cart
    {
        [BsonRequired]
        public string ProductId { get; set; } = null!;

        [BsonRequired]
        public int ProductQuantity { get; set; }
    }
}
