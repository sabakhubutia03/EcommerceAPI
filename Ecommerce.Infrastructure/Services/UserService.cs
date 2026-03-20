using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class UserService : IUserService
{
    public readonly ApplicationDbContext _context;
    public readonly ILogger<UserService> _logger;

    public UserService(ApplicationDbContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var getAllUsers = await _context.Users.ToListAsync();
        if (getAllUsers == null || !getAllUsers.Any())
        {
            _logger.LogInformation("No Users Found");
            return new List<UserDto>();
        }
        var users = getAllUsers.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            CreatedAt = u.CreatedAt,
        }).ToList();
        return users;
    }

    public  async Task<UserDto> CreateUser(UserCreateDto userCreateDto)
    {
        if (string.IsNullOrWhiteSpace(userCreateDto.Name))
        {
            _logger.LogError("User Name null or empty");
            throw new Exception("User Name null or empty");
        }

        if (string.IsNullOrWhiteSpace(userCreateDto.Email))
        {
            _logger.LogError("User Email null or empty");
            throw new Exception("User Email null or empty");
        }

        if (string.IsNullOrWhiteSpace(userCreateDto.PasswordHash))
        {
            _logger.LogError("Password Hash null or empty");  
            throw new Exception("Password Hash null or empty");
        }
        
        var exists = await _context.Users.AnyAsync(u => u.Email == userCreateDto.Email);
        if (!exists)
        {
            _logger.LogError("User already exists email {Email}", userCreateDto.Email);
            throw new Exception("User already exists email");
        }

        var usernew = new User
        {
            Name = userCreateDto.Name,
            Email = userCreateDto.Email,
            PasswordHash = userCreateDto.PasswordHash,
            CreatedAt = DateTime.Now,
        };
        await _context.Users.AddAsync(usernew);
        await _context.SaveChangesAsync();

        return new UserDto
        {
            Id = usernew.Id,
            Name = usernew.Name,
            Email = usernew.Email,
            CreatedAt = usernew.CreatedAt,
        };

    }

    public Task<UserDto?> GetUserById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}