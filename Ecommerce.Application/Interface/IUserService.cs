using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interface;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<UserDto> CreateUser(UserCreateDto userCreateDto);
    Task<UserDto?> GetUserById(int id);
    Task<UserDto> UpdateUser(int id,UserUpdateDto userUpdateDto);
    Task<bool> DeleteUser(int id);
}