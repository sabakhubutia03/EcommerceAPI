using Ecommerce.Application.DTOs;
using FluentValidation;

namespace Ecommerce.Application.Validators;

public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator()
    {
        RuleFor(n => n.Name)
            .MinimumLength(3).WithMessage("Name must be at least 3 characters")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters")
            .When(n => n.Name != null);

        RuleFor(e => e.Email)
            .EmailAddress().WithMessage("Email must be a valid email address")
            .When(e => e.Email != null);
        
        RuleFor(p => p.PasswordHash)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .When(p => p.PasswordHash  != null);
    }
}