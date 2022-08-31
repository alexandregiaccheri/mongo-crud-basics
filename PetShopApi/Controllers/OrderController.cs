using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
using PetShopApi.Services;

namespace PetShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] Order order)
        {
            return Ok(await _orderService.CreateOrderAsync(order));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(string id)
        {
            var result = await _orderService.GetOrderAsync(id);
            if (result == null)
                return NotFound();
            return Ok(await _orderService.DeleteOrderAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            var result = await _orderService.GetOrderAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(string id, [FromBody] Order order)
        {
            var result = await _orderService.GetOrderAsync(id);
            if (result == null)
                return NotFound();
            return await _orderService.UpdateOrderAsync(id, order);
        }
    }
}
