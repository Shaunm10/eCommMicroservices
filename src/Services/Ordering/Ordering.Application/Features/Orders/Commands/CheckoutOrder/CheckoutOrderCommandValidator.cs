using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder;

public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        this.RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("{UserName} is required.")
            .NotNull()
            .MaximumLength(50)
            .WithMessage("{UserName} must not exceed 50 characters.");

        this.RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithMessage("{EmailAddress} is required.");

        this.RuleFor(x => x.TotalPrice)
            .NotEmpty()
            .WithMessage("{TotalPrice} is required.")
            .GreaterThan(0)
            .WithMessage("{TotalPrice} should be greater than zero.");
    }
}
