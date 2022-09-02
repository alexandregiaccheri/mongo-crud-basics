using Microsoft.AspNetCore.Mvc;
using PetShopApi.Models;
using PetShopApi.Models.DTO;
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

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <remarks>
        /// These values cannot be updated later (with the exception of the order status and payment status).<br/>        
        /// Returns the newly created order entry.
        /// </remarks>
        /// <param name="dto">Payload consisting of multiple fields, most will become readonly after the order creation.</param>     
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            if (dto.OrderItems == null || dto.OrderItems.Count == 0)
                return BadRequest();

            else
                return Ok(await _orderService.CreateOrderAsync(dto));
        }

        /// <summary>
        /// Deletes an existing order.
        /// </summary>
        /// <remarks>
        /// This wouldn't make sense in a production environment, but it was implemented to test the database integration.<br/>
        /// Deletes an order based on it's id. Returns the deleted order entry.
        /// </remarks>
        /// <param name="id">The id of an existing order. Must be a valid 24 digits hex value.</param>        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(string id)
        {
            var result = await _orderService.GetOrderAsync(id);
            if (result == null)
                return NotFound();

            return Ok(await _orderService.DeleteOrderAsync(id));
        }

        /// <summary>
        /// Retrieves a list with all orders.
        /// </summary>
        /// <remarks>
        /// Returns the list with all orders.
        /// </remarks>        
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();

            return Ok(result);
        }

        /// <summary>
        /// Retrieves an order based on it's id.
        /// </summary>
        /// <remarks>
        /// Returns the order if it exists.
        /// </remarks>
        /// <param name="id">The id of an existing order. Must be a valid 24 digits hex value.</param> 
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(string id)
        {
            var result = await _orderService.GetOrderAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Finds and updates an order with new status.
        /// </summary>
        /// <remarks>
        /// Returns the updated order or the original order if no changes were made.<br/>
        /// Changes will only be commited if the payload contains a new value for the order or payment status.<br/>        
        /// </remarks>
        /// <param name="id">The id of an existing order. Must be a valid 24 digits hex value.</param>
        /// <param name="dto">Payload with the new status for the existing order.<br/></param>       
        [HttpPatch("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(string id, [FromBody] UpdateOrderDTO dto)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order == null)
                return NotFound();

            return await _orderService.UpdateOrderAsync(id, order, dto);
        }
    }
}
