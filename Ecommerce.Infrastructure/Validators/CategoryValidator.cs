using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Infrastructure.Validators;

public class CategoryValidator
{
    public void ValidateCreateCategory(CategoryCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ApiException(
                "Name null or empty",
                "BadRequest",
                400,
                "Name is Null or Empty",
                "/api/users/CreateCategory"
            );
        }

        if (dto.Name.Length < 3)
        {
            throw new ApiException(
                "Name is too short",
                "BadRequest",
                400,
                "Category name must be at least 3 characters",
                "/api/users/CreateCategory"
            );
        }
    }

    public void ValidateUpdateCategory(CategoryUpdateDto dto)
    {
        if (dto.Name != null && string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ApiException(
                "Name is null or empty",
                "BadRequest",
                400,
                "Updated name is invalid",
                "/api/users/UpdateCategory"
            );
        }
    }
}