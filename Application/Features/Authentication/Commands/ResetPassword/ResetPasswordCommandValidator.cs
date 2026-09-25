using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Application.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommand ()
        {
            RuleFor( e => e.UserId)
                .NotEmpty()
                .WithMessage("User ID is required.");

            RuleFor( e => e.Token)
                .NotEmpty()
                .WithMessage("Reset token is required.");

            RuleFor( e => e.NewPassword)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters.");
        }
    }
}
