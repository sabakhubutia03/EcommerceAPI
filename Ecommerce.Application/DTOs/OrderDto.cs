
namespace Ecommerce.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } =  DateTime.UtcNow; 
    
    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}