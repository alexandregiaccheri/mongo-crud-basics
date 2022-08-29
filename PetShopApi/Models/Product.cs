using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetShopApi.Models
{
    public class Product
    {
        [BsonRequired]
        public DateTime CreationDate { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string ImgUrl { get; set; } = null!;

        [BsonRequired]
        public DateTime LastUpdated { get; set; }

        [BsonRequired]
        public string Manufacturer { get; set; } = null!;
        
        [BsonRequired]
        public string ProductDescription { get; set; } = null!;
        
        [BsonRequired]
        public int ProductPrice { get; set; }

        [BsonRequired]
        public string ProductTitle { get; set; } = null!;

        [BsonRequired]
        public int Stock { get; set; }
    }
}
