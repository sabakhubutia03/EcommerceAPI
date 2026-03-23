using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers;
[ApiController]
[Route("api/Products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateProduct(ProductCreateDto product)
    {
        var createProduct = await _productService.CreateProduct(product);
        return CreatedAtAction(nameof(GetProductById) , new { id = createProduct.Id }, createProduct);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var allProducts = await _productService.GetAllProducts();
        return Ok(allProducts);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>>GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProduct(int id, ProductUpdateDto product)
    {
        var  updateProduct = await _productService.UpdateProduct(id, product);
        return Ok(updateProduct);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var deleteProduct = await _productService.DeleteProduct(id);
        return NoContent();
    } 
}