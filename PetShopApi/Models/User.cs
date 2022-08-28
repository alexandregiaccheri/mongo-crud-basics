using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PetShopApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string UserName { get; set; } = null!;

        [BsonRequired]
        public string UserEmail { get; set; } = null!;

        [BsonRequired]
        public DateTime CreationDate { get; set; }

        [BsonRequired]
        public DateTime LastUpdated { get; set; }
    }
}
