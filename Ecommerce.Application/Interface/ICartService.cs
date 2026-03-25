using Ecommerce.Application.DTOs;


namespace Ecommerce.Application.Interface;

public interface ICartService
{
    Task<CartDto>GetCartByUserId(int userId);
    Task<CartItemDto> CreateCarItem(int userId,CartItemCreateDto dto);
    Task<CartItemDto> UpdateCartItem(int cartItemId, CartItemUpdateDto dto);
    Task<bool> DeleteCartItem(int cartItemId);
    Task<bool> DeleteCart(int userId);
    
}