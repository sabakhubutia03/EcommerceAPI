using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Infrastructure.Validators;

public class ProductValidator
{
    public void ValidateCreateProduct(ProductCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ApiException(
                "Product name cannot be null or empty.",
                "BadRequest",
                400,
                "Product name cannot be null or empty.",
                "/api/Products/CreateProduct"
                );
        }

        if (string.IsNullOrWhiteSpace(dto.Description))
        {
            throw new ApiException(
                "Product description cannot be null or empty.",
                "BadRequest",
                400,
                "Product description cannot be null or empty.",
                "/api/Products/CreateProduct"
            );
        }

        if (dto.Price < 0)
        {
            throw new ApiException(
                "Product price cannot be negative.",
                "BadRequest",
                400,
                "Product price cannot be negative.",
                "/api/Products/CreateProduct"
            );
        }

        if (dto.Stock <= 0)
        {
            throw new ApiException(
                "Product stock cannot be negative.",
                "BadRequest",
                400,
                "Product stock cannot be negative.",
                "/api/Products/CreateProduct"
            );
        }

        if (dto.CategoryId <= 0)
        {
            throw new ApiException(
                "Product category cannot be negative.",
                "BadRequest",
                400,
                "Product category cannot be negative.",
                "/api/Products/CreateProduct"
            );
        }
        
    }

    public void ValidateUpdateProduct(ProductUpdateDto dto)
    {
        if (dto.Name != null && string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ApiException(
                "Product name cannot be null or empty.",
                "BadRequest",
                400,
                "Product name cannot be null or empty.",
                "/api/Products/UpdateProduct"
            );
        }

        if (dto.Description != null && string.IsNullOrWhiteSpace(dto.Description))
        {
            throw new ApiException(
                "Product description cannot be null or empty.",
                "BadRequest",
                400,
                "Product description cannot be null or empty.",
                "/api/Products/UpdateProduct"
            );
        }

        if (dto.Price != null && dto.Price < 0)
        {
            throw new ApiException(
                "Product price cannot be negative.",
                "BadRequest",
                400,
                "Product price cannot be negative.",
                "/api/Products/UpdateProduct"
            );
        }

        if (dto.Stock != null && dto.Stock < 0)
        {
            throw new ApiException(
                "Product stock cannot be negative.",
                "BadRequest",
                400,
                "Product stock cannot be negative.",
                "/api/Products/UpdateProduct"
            );
        }
    }
}