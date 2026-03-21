using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Infrastructure.Validators;

public class UserValidator
{
    public void ValidateCreateUser(UserCreateDto dto)
    {
        
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ApiException(
                "User Name null or empty",
                "BadRequest",
                400,
                "Name is Null or Empty",
                "/api/users/CreateUser");
        }

        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new ApiException(
                "User Email null or empty",
                "BadRequest",
                400,
                "Email is Null or Empty",
                "/api/users/CreateUser"
            );
        }

        if (string.IsNullOrWhiteSpace(dto.PasswordHash))
        {
            throw new ApiException(
                "Password Hash null or empty",
                "BadRequest",
                400,
                "Password is Null or Empty",
                "/api/users/CreateUser"
            );
        }
    } 
    
    public void ValidateUpdateUser(UserUpdateDto dto)
    {
        if (dto.Name != null && string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ApiException(
                "Name is null or empty",
                "BadRequest",
                400,
                "Name is Null or Empty",
                "/api/users/UpdateUser"
            );
        }

        if (dto.Email != null &&! dto.Email.Contains("@"))
        {
            throw new ApiException(
                "Email is null or empty",
                "ValidationError",
                400,
                "Email is Null or Empty",
                "/api/users/UpdateUser"
            );
        }

        if (dto.PasswordHash != null && dto.PasswordHash.Length < 6)
        {
            throw new ApiException(
                "PasswordHash is null or empty",
                "ValidationError",
                400,
                "PasswordHash is Null or Empty",
                "/api/users/UpdateUser"
            );
        }
    }
}