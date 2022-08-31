namespace PetShopApi.Models
{
    public class MongoDbSettings
    {
        public string CartCollection { get; set; } = null!;

        public string CategoryCollection { get; set; } = null!;

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProductCollection { get; set; } = null!;

        public string UserCollection { get; set; } = null!;
    }
}
