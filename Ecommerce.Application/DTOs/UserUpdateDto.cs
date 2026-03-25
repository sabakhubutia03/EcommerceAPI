namespace Ecommerce.Application.DTOs;

public class UserUpdateDto
{
    public string? Email { get; set; }
    public string? Name { get; set; }
    public string? PasswordHash { get; set; }
}