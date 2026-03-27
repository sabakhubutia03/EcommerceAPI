using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interface;

public interface IOrderService 
{
    Task<IEnumerable<OrderDto>> GetAllOrders();
    Task<IEnumerable<OrderDto>> GetUserOrderHistory (int userId);
    Task<OrderDto> CreateOrder(int userId,OrderCreateDto orderCreateDto);
    Task<OrderDto?> GetOrderById(int id);
    Task<bool> DeleteOrder(int id);
    Task<bool> DeleteOrdersByUserId (int UserId);
}