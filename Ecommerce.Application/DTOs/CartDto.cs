
namespace Ecommerce.Application.DTOs;

public class CartDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
   
}