using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PetShopApi.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        public string CategoryName { get; set; } = null!;

        [BsonRequired]
        public DateTime CreationDate { get; set; }

        //TODO: Adicionar LastUpdated e implementar CreationDate corretamente
    }    
}
