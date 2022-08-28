using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Models;

namespace PetShopApi.Services
{
    public class UserService : IUserService<User>
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            var mongoClient = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<User>(mongoDbSettings.Value.UserCollection);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            user.CreationDate = DateTime.Now;
            user.LastUpdated = DateTime.Now;
            await _userCollection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> DeleteUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var user = await _userCollection.Find(filter).SingleAsync();
            await _userCollection.DeleteOneAsync(filter);
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<User> GetUserAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _userCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<User> UpdateUserAsync(string id, User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var result =
            await _userCollection.UpdateOneAsync(filter, Builders<User>
                .Update.Set(u => u.UserName, user.UserName)
                       .Set(u => u.UserEmail, user.UserEmail)
                       .Set(u => u.LastUpdated, DateTime.Now));
            return await _userCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
