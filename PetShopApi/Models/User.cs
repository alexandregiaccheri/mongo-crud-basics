using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetShopApi.Models
{
    public class User
    {
        [BsonRequired]
        public DateTime CreationDate { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public DateTime LastUpdated { get; set; }

        [BsonRequired]
        public string UserEmail { get; set; } = null!;

        [BsonRequired]
        public string UserName { get; set; } = null!;
    }
}
