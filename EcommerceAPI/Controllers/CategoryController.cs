using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[ApiController]
[Route("api/Category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory(CategoryCreateDto dto)
    {
        var createCategory = await _categoryService.CreateCategory(dto);
        return CreatedAtAction(nameof(GetCategoryById) , new { id = createCategory.Id }, createCategory);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var getAllCategories = await _categoryService.GetAllCategories();
        return Ok(getAllCategories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryById(int id)
    {
        var getCategoryById = await _categoryService.GetCategoryById(id);
        return Ok(getCategoryById);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryUpdateDto dto)
    {
        var updateCategory = await _categoryService.UpdateCategory(id, dto);
        return Ok(updateCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(int id)
    {
        var deleteCategory = await _categoryService.DeleteCategory(id);
        return NoContent();
    }
}