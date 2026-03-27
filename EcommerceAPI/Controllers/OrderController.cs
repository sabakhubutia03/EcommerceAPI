using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[ApiController]
[Route("api/Orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            return Ok(order);
        }

        [HttpPost("{userId:int}")]
        public async Task<ActionResult<OrderDto>> CreateOrder(int userId, [FromBody] OrderCreateDto order)
        {
            var createdOrder = await _orderService.CreateOrder(userId, order);
            return Ok(createdOrder);
        }

        [HttpGet("history/{userId:int}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetUserOrderHistory(int userId)
        {
            var orders = await _orderService.GetUserOrderHistory(userId);
            return Ok(orders);
        }
        
        
        [HttpDelete("user/{userId:int}")]
        public async Task<ActionResult> DeleteOrdersByUserId(int userId)
        {
            await _orderService.DeleteOrdersByUserId(userId);   
            return NoContent();
        }
    
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
