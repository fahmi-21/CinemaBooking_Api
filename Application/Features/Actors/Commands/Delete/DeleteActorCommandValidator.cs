using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Actors.Commands.Delete
{
    public sealed class DeleteActorCommandValidator : AbstractValidator<DeleteActorCommand>
    {
        public DeleteActorCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Actor ID is required.");
        }
    }
}