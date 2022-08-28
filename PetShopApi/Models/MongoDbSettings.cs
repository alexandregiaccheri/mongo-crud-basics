namespace PetShopApi.Models
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string CategoryCollection { get; set; } = null!;
    }
}
