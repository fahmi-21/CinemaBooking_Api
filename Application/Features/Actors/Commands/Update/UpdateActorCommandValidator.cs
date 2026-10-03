using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Actors.Commands.Update
{
    public sealed class UpdateActorCommandValidator : AbstractValidator<UpdateActorCommand>
    {
        public UpdateActorCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Actor ID is required.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(100)
                .WithMessage("Full name must not exceed 100 characters.");

            RuleFor(x => x.PhotoUrl)
                .MaximumLength(500)
                .WithMessage("Photo URL must not exceed 500 characters.");
        }
    }
}
