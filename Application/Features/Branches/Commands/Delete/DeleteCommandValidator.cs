using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Delete
{
    public sealed class DeleteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DeleteCommandValidator ()
        {
            RuleFor(e => e.Id)
                .GreaterThan(0)
                .WithMessage("Branch ID must be greater than zero.");
        }
    }
}
