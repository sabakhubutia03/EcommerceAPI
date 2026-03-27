using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators;

public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    public OrderCreateValidator()
    {
        RuleFor(s => s.ShippingAddress)
            .NotEmpty().WithMessage("Address cannot be empty");
        RuleFor(s => s.PhoneNumber)
            .NotEmpty().WithMessage("Phone number cannot be empty")
            .MinimumLength(9).WithMessage("Phone number must be at least 9 characters long");
    }
}