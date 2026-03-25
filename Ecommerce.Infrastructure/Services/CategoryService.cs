using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context; 

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        var getAllCategories = await _context.Categories.ToListAsync();
        if (getAllCategories == null || !getAllCategories.Any())
        {
            return new List<CategoryDto>();
        }
        
        var categoryDtos = getAllCategories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
        }).ToList();
        return categoryDtos;
    }

    public async Task<CategoryDto?> GetCategoryById(int id)
    {
        var getCategoryById = await _context.Categories.FindAsync(id);
        if (getCategoryById == null)
        {
            return null;
        }

        return new CategoryDto
        {
            Id = getCategoryById.Id,
            Name = getCategoryById.Name,
        };
    }

    public async Task<CategoryDto> CreateCategory(CategoryCreateDto categoryCreateDto)
    {

        var category = new Category
        {
            Name = categoryCreateDto.Name,
        };
        
        await _context.AddAsync(category);
        await _context.SaveChangesAsync();

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
        };
    }

    public  async Task<CategoryDto> UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
    {
        var categoryUpdate =  await _context.Categories.FindAsync(id);
        if (categoryUpdate == null)
        {
            throw new ApiException(
                "Category not found",
                "NotFound",
                404,
                $"Category not found id {id}",
                "/api/Category/GetCategoryById"
                );
        }

        if (!string.IsNullOrEmpty(categoryUpdateDto.Name))
            categoryUpdate.Name = categoryUpdateDto.Name;
        
        await _context.SaveChangesAsync();
        
        return new CategoryDto
        {
            Id = categoryUpdate.Id,
            Name = categoryUpdate.Name,
        };

    }

    public async Task<bool> DeleteCategory(int id)
    {
        var deleteCategory = await _context.Categories.FindAsync(id);
        if (deleteCategory == null)
        {
            throw new ApiException(
                "Category not found",
                "NotFound",
                404,
                $"Category with ID {id} does not exist and cannot be deleted",
                "/api/Category/GetCategoryById"
            );
        }
        
        _context.Categories.Remove(deleteCategory);
        
        await _context.SaveChangesAsync();
        return true;
        
    }
}