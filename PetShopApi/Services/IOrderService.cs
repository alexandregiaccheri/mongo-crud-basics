using PetShopApi.Models.DTO;

namespace PetShopApi.Services
{
    public interface IOrderService<Order>
    {
        Task<Order> CreateOrderAsync(CreateOrderDTO dto);

        Task<Order> DeleteOrderAsync(string id);

        Task<List<Order>> GetAllOrdersAsync();

        Task<Order> GetOrderAsync(string id);

        Task<Order> UpdateOrderAsync(string id, Order order, UpdateOrderDTO dto);
    }
}
