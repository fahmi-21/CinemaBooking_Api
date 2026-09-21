using FluentValidation;
namespace Application.Features.Authentication.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(e => e.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(4)
            .WithMessage("First name is required.");

        RuleFor(e => e.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .MinimumLength(4)
            .WithMessage("Last name is required.");

        RuleFor(e => e.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(e => e.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");

        RuleFor(e => e.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm password is required.")
            .Equal(e => e.Password)
            .WithMessage("Passwords do not match.");
    }
}

