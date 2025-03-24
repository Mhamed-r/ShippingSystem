using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Api.Infrastructure.Dtos;
using Shipping.Api.Services;

namespace Shipping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly OrderServices _orderService;

        public OrderController(OrderServices orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("AllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsyncAndDeleteOrder();
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order is null) return NotFound("Order not found");
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] addOrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _orderService.CreateOrderAsync(orderDto);
           // if (!result) return BadRequest("Failed to create order");

            return Created();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] updateOrderDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _orderService.UpdateOrderAsync(orderDto);
            if (!result) return NotFound("Order not found");
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result) return NotFound(new { message = "Order not found or already deleted" });
            return Ok(new { message = "Order soft deleted successfully" });
        }


        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreOrder(int id)
        {
            var result = await _orderService.RestoreOrderAsync(id);
            if (!result) return NotFound(new { message = "Order not found or not deleted" });

            return Ok(new { message = "Order restored successfully" });
        }
    }

}