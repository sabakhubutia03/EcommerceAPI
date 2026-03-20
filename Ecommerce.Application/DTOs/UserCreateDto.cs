namespace Ecommerce.Application.DTOs;

public class UserCreateDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}