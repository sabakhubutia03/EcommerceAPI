using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Services;

public class OrderService : IOrderService
{ 
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrderService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {
        var orders = await _dbContext.Orders.
            Include(O => O.OrderItems).
            ThenInclude(p => p.Product).
            ToListAsync();
        if (!orders.Any())
        {
           return Enumerable.Empty<OrderDto>();
        }
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<IEnumerable<OrderDto>> GetUserOrderHistory(int userId)
    {
        var orderByUserId = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(p => p.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();
        if (orderByUserId == null || !orderByUserId.Any())
        {
            return Enumerable.Empty<OrderDto>();
        }
        return _mapper.Map<IEnumerable<OrderDto>>(orderByUserId);
    }

    public async Task<OrderDto> CreateOrder(int userId, OrderCreateDto orderCreateDto)
    {
        var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null || !cart.CartItems.Any())
        {
            throw new ApiException("Cart is empty",
                "BadRequest",
                400,
                "Cannot checkout with an empty cart", 
                "/api/orders/create"
                );
        }
        
        if (cart.CartItems.Any(ci => ci.Product == null))
        {
            throw new ApiException(
                "One or more products in your cart no longer exist",
                "BadRequest",
                400,
                "not Found",
                "/api/orders/create"
                );
        }

        var orderNew = new Order
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            TotalPrice = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity),
            OrderItems = new List<OrderItem>(),
        };

        foreach (var cartItem in cart.CartItems)
        {
          
            if (cartItem.Product.Stock < cartItem.Quantity)
            {
                throw new ApiException(
                    $"Not enough stock for {cartItem.Product.Name}",
                    "BadRequest",
                    400,
                    "not enough stock",
                    "/api/orders/create"
                    );
            }

            var orderItem = new OrderItem
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                Price = cartItem.Product.Price 
            };

            cartItem.Product.Stock -= cartItem.Quantity;
            orderNew.OrderItems.Add(orderItem);
        }

        await _dbContext.Orders.AddAsync(orderNew);
        _dbContext.CartItems.RemoveRange(cart.CartItems); 
        await _dbContext.SaveChangesAsync();
    
        return _mapper.Map<OrderDto>(orderNew);
    }
   

    public async Task<OrderDto?> GetOrderById(int id)
    {
        var getOrder = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (getOrder == null)
        {
            throw new ApiException(
                "Order not found",
                "NotFound",
                404,
                "The requested order does not exist.",
                "/api/orders/GetOrderById"
            );
        }
        
        return _mapper.Map<OrderDto>(getOrder);
    }

    public async Task<bool> DeleteOrder(int id)
    {
        var deleteOrder = await _dbContext.Orders.FindAsync(id);
        if (deleteOrder == null)
        {
            throw new ApiException(
                "Order not found",
                "NotFound",
                404,
                "not found",
                "/api/orders/DeleteOrder"
            );
        }
        _dbContext.Orders.Remove(deleteOrder);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteOrdersByUserId(int UserId)
    {
        var deletOrderUser = await _dbContext.Orders
            .Include(i => i.OrderItems)
            .Where(u => u.UserId == UserId)
            .ToListAsync();
        if (deletOrderUser == null || !deletOrderUser.Any())
        {
            throw new ApiException(
                "Order not found",
                "NotFound",
                404,
                "not found",
                "/api/orders/DeleteByUserId"
            );
        }

        _dbContext.Orders.RemoveRange(deletOrderUser);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}