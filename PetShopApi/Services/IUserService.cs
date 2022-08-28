namespace PetShopApi.Services
{
    public interface IUserService<User>
    {
        Task<User> CreateUserAsync(User category);

        Task<User> DeleteUserAsync(string id);

        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserAsync(string id);

        Task<User> UpdateUserAsync(string id, User category);
    }
}
