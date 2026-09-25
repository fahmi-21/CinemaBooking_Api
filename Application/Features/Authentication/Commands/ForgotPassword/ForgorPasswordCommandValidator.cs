using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Application.Features.Authentication.Commands.ForgotPassword
{
    public sealed class ForgotPasswordCommandValidator : AbstractValidator<ForgorPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email address is required.");
        }
    }
}
