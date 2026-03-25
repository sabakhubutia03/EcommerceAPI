using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[ApiController]
[Route("api/Cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("add/{userId}")]
    public async Task<ActionResult> Create(int userId, [FromBody] CartItemCreateDto dto)
    {
        var result = await _cartService.CreateCarItem(userId, dto);
        return Ok(result);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<CartDto>> GetById(int userId)
    {
        var result = await _cartService.GetCartByUserId(userId);
        return Ok(result);
    }

    [HttpPut("item/{cartItemId}")]
    public async Task<ActionResult<CartItemDto>> Update(int cartItemId, CartItemUpdateDto dto)
    {
        var result = await _cartService.UpdateCartItem(cartItemId, dto);
        return Ok(result);
    }

    [HttpDelete("item/{cartItemId}")]
    public async Task<ActionResult> Delete(int cartItemId)
    { 
        await _cartService.DeleteCartItem(cartItemId);
        return NotFound();
    }

    [HttpDelete("clear/{userId}")]
    public async Task<ActionResult> DeleteCartItem(int userId)
    {
         await _cartService.DeleteCart(userId);
        return NotFound();
    }
    
}