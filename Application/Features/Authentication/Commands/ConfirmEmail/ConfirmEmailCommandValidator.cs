using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Confirmation token is required.");
        }
    }
}
