using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetShopApi.Models
{
    public class Category
    {
        [BsonRequired]
        public string CategoryName { get; set; } = null!;

        [BsonRequired]
        public DateTime CreationDate { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public DateTime LastUpdated { get; set; }

        [BsonRequired]
        public string Slug { get; set; } = null!;
    }
}
