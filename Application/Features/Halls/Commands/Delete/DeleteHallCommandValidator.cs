using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Delete
{
    public sealed class DeleteHallCommandValidator : AbstractValidator<DeleteHallCommand>
    {
        public DeleteHallCommandValidator ()
        {
            RuleFor(e => e.Id)
                .GreaterThan(0)
                .WithMessage("hall id must be greater than zero");
        }
    }
}
