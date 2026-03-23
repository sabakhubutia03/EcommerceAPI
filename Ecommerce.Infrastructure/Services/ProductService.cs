using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Infrastructure.Data;
using Ecommerce.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Services;

public class ProductService : IProductService
{ 
    private readonly ApplicationDbContext _context;
    private readonly ProductValidator _validator;

    public ProductService(ApplicationDbContext context, ProductValidator validator)
    {
        _context = context;
        _validator = validator;
    }
    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        var getAllProduct = await _context.Products.ToListAsync();
        if (getAllProduct == null || !getAllProduct.Any())
        {
            return new List<ProductDto>();
        }
        var products = getAllProduct.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            CategoryId = p.CategoryId,
        });
        return products;
    }

    public async Task<ProductDto?> GetProductById(int id)
    {
        var getByIdProduct = await _context.Products.FindAsync(id);
        if (getByIdProduct == null)
        {
            return null;
        }

        return new ProductDto
        {
            Id = getByIdProduct.Id,
            Name = getByIdProduct.Name,
            Description = getByIdProduct.Description,
            Price = getByIdProduct.Price,
            Stock = getByIdProduct.Stock,
            CategoryId = getByIdProduct.CategoryId,
        };
    }

    public async Task<ProductDto> CreateProduct(ProductCreateDto product)
    {
        _validator.ValidateCreateProduct(product);
        
        var categoryId = await _context.Categories.FirstOrDefaultAsync(
            c => c.Id == product.CategoryId);
        if (categoryId == null)
        {
            throw new ApiException(
                "Category not found",
                "BadRequest",
                400,
                "Invalid CategoryId",
                "/api/Product/CreateProduct"
                );
        }
        var products = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
        };
         await _context.Products.AddAsync(products);
         await _context.SaveChangesAsync();

         return new ProductDto
         {
             Id = products.Id,
             Name = products.Name,
             Description = products.Description,
             Price = products.Price,
             Stock = products.Stock,
             CategoryId = products.CategoryId,
         };

    }

    public async Task<ProductDto> UpdateProduct(int id, ProductUpdateDto product)
    {
        _validator.ValidateUpdateProduct(product);
        var productUpdateById = await _context.Products.FindAsync(id);
        if (productUpdateById == null)
        {
            throw new ApiException(
                "Product not found",
                "NotFound",
                404,
                "Product not found",
                "/api/Product/GetProductById"
                );
        }

        if (!string.IsNullOrWhiteSpace(product.Name))
        {
            productUpdateById.Name = product.Name;
        }

        if (product.Description != null)
        {
            productUpdateById.Description = product.Description;
        }

        if (product.Price.HasValue)
        {
            productUpdateById.Price = product.Price.Value;
        }

        if (product.Stock.HasValue)
        {
            productUpdateById.Stock = product.Stock.Value;
        }
        await _context.SaveChangesAsync();

        return new ProductDto
        {
            Id = productUpdateById.Id,
            Name = productUpdateById.Name,
            Description = productUpdateById.Description,
            Price = productUpdateById.Price,
            Stock = productUpdateById.Stock,
            CategoryId = productUpdateById.CategoryId,
        };
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var deleteProduct = await _context.Products.FindAsync(id);
        if (deleteProduct == null)
        {
            throw new ApiException(
                "Product not found",
                "NotFound",
                404,
                $"Product not found id {id}",
                "/api/Product/DeleteProduct"
            );
        }

        _context.Products.Remove(deleteProduct);
        await _context.SaveChangesAsync();
        return true;
    }
}