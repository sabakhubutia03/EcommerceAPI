using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserService> _logger;

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
        var exists = await _context.Users.AnyAsync(u => u.Email == userCreateDto.Email);
        if (exists)
        {
            _logger.LogError("User already exists email {Email}", userCreateDto.Email);
            throw new ApiException( 
                "User already exists email",
                "Conflict",
                409,
                $"Email already exists : {userCreateDto.Email}",
                "/api/users/CreateUser"
            );
        }

        var usernew = new User
        {
            Name = userCreateDto.Name,
            Email = userCreateDto.Email,
            PasswordHash = userCreateDto.PasswordHash,
            CreatedAt = DateTime.UtcNow,
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

    public  async Task<UserDto?> GetUserById(int id)
    {
        var getByUserId = await _context.Users.FindAsync(id);
        if (getByUserId == null)
        {
            _logger.LogInformation("No Users Found");
            return null;
        }

        return new UserDto
        {
            Id = getByUserId.Id,
            Name = getByUserId.Name,
            Email = getByUserId.Email,
            CreatedAt = getByUserId.CreatedAt,
        };
    }

    public async Task<UserDto> UpdateUser(int id, UserUpdateDto userUpdateDto)
    {
        
        var updateUser = await _context.Users.FindAsync(id);
        if (updateUser == null)
        {
            _logger.LogWarning("No Users Found id {Id}", id);
            throw new ApiException(
                "User Not Found", 
                "NotFound", 
                404, 
                $"No user found with id: {id}", 
                "/api/users/UpdateUser"
            );
        }

        if (!string.IsNullOrEmpty(userUpdateDto.Name))
        {
            updateUser.Name = userUpdateDto.Name;
        }

        if (userUpdateDto.Email != null)
        {
            updateUser.Email = userUpdateDto.Email;
        }


        if (userUpdateDto.PasswordHash != null)
        {
            updateUser.PasswordHash = userUpdateDto.PasswordHash;
        }
        
        await _context.SaveChangesAsync();
        return new UserDto
        {
            Id = updateUser.Id,
            Name = updateUser.Name,
            Email = updateUser.Email,
            CreatedAt = updateUser.CreatedAt,
        };
    }

    public async Task<bool> DeleteUser(int id)
    {
        var deleteUser = await _context.Users.FindAsync(id);
        if (deleteUser == null)
        {
            _logger.LogWarning("User id not  found {Id}", id);
            return false;
        }

        _context.Users.Remove(deleteUser);
        await _context.SaveChangesAsync();
        return true;
    }
}