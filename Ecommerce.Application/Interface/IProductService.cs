using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interface;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllProducts();
    Task<ProductDto?> GetProductById(int id);
    Task<ProductDto> CreateProduct(ProductCreateDto product);
    Task<ProductDto> UpdateProduct(int id,ProductUpdateDto product);
    Task<bool>DeleteProduct(int id);
}