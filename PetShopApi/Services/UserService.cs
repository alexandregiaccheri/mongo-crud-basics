using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using PetShopApi.Models;
using PetShopApi.Models.DTO;

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

        public async Task<User> CreateUserAsync(CreateUserDTO dto)
        {
            var user = new User()
            {
                CreationDate = DateTime.Now,
                LastUpdated = DateTime.Now,
                UserEmail = dto.UserEmail!,
                UserName = dto.UserName!
            };
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

        public async Task<User> UpdateUserAsync(string id, User user, UpdateUserDTO dto)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var changesWereMade = false;

            if (!string.IsNullOrEmpty(dto.UserEmail)
                && dto.UserEmail != user.UserEmail)
            {
                await _userCollection.UpdateOneAsync(filter, Builders<User>
                    .Update.Set(u => u.UserEmail, dto.UserEmail));

                changesWereMade = true;
            }

            if (!string.IsNullOrEmpty(dto.UserName)
                && dto.UserName != user.UserName)
            {
                await _userCollection.UpdateOneAsync(filter, Builders<User>
                    .Update.Set(u => u.UserName, dto.UserName));

                changesWereMade = true;
            }

            if (changesWereMade)
                await _userCollection.UpdateOneAsync(filter, Builders<User>
                    .Update.Set(u => u.LastUpdated, DateTime.Now));

            return await _userCollection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
