using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interface;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllCategories();
    Task<CategoryDto?> GetCategoryById(int id);
    Task<CategoryDto> CreateCategory (CategoryCreateDto categoryCreateDto);
    Task<CategoryDto> UpdateCategory (int id, CategoryUpdateDto categoryUpdateDto);
    Task<bool> DeleteCategory(int id);
}