using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Services;

public class CartService : ICartService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CartService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<CartDto> GetCartByUserId(int userId)
    {
        var cart = await _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart == null)
        {
            throw new ApiException("Cart not found",
                "Cart not found",
                404,
                "Cart not found",
                "/api/Cart/GetCartByUserId"
            );
        }

        return _mapper.Map<CartDto>(cart);
        
    }

    public async Task<CartItemDto> CreateCarItem(int userId, CartItemCreateDto dto)
    {
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        
        if (cart == null)
        {
            cart = new Cart { UserId = userId };
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }

      
        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.CartId == cart.Id && c.ProductId == dto.ProductId);

        if (existingItem == null) 
        {
           
            var newItem = _mapper.Map<CartItem>(dto);
            newItem.CartId = cart.Id; 
            await _context.CartItems.AddAsync(newItem);
            existingItem = newItem; 
        }
        else 
        {
            existingItem.Quantity += dto.Quantity;
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<CartItemDto>(existingItem);
    }

    public  async Task<CartItemDto> UpdateCartItem(int cartItemId, CartItemUpdateDto dto)
    {
        var updateCartItem = await _context.CartItems.FindAsync(cartItemId);
        if (updateCartItem == null)
        {
            throw new ApiException(
                "Cart item not found",
                "BedRequest",
                404,
                "Cart item not found",
                "/api/Cart/UpdateCartItem"
            );
        }

        if (dto.Quantity > 0)
        {
            updateCartItem.Quantity = dto.Quantity;
        }
        await _context.SaveChangesAsync();
        return _mapper.Map<CartItemDto>(updateCartItem);
    }

    public async Task<bool> DeleteCartItem(int cartItemId)
    {
        var deleteCartItem = await _context.CartItems.FindAsync(cartItemId);
        if (deleteCartItem == null)
        {
            throw new ApiException(
                "Cart item not found",
                "NotFound",
                404,
                "Cart item not found",
                "/api/Cart/DeleteCartItem"
            );
        }
        _context.CartItems.Remove(deleteCartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public  async Task<bool> DeleteCart(int userId)
    {
        var cart = await _context.Carts.Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId);

        if (cart != null && cart.CartItems.Any())
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
        }
        return true;
    }
}