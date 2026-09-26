using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Application.Features.Authentication.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator ()
        {
            RuleFor(e => e.CurrentPassword)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(e => e.NewPassword)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.NewPassword)
            .NotEqual(x => x.CurrentPassword)
            .WithMessage("New password must be different from current password.");
        }
    }
}
