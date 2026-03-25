using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateValidator()
    {
        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters")
            .When(n => n.Name != null);
            
        
        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Description is required")
            .MinimumLength(3).WithMessage("Description must be at least 3 characters long")
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters")
            .When(d => d.Stock.HasValue);
        
        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0")
            .When(p => p.Price.HasValue);
        
        RuleFor(s =>s.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than 0")
            .When(s => s.Stock.HasValue);
            
    }
}