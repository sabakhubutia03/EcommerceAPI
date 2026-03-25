using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators;

public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateValidator()
    {
        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");
        
        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters");
        
        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        RuleFor(s => s.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal 0"); 
        
        RuleFor(c => c.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be a valid ID (greater than 0)");
    }
}