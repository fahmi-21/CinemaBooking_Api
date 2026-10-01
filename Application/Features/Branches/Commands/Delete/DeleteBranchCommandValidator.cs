using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Delete
{
    public sealed class DeleteBranchCommandValidator : AbstractValidator<DeleteBranchCommand>
    {
        public DeleteBranchCommandValidator()
        {
            RuleFor(e => e.Id)
                .GreaterThan(0)
                .WithMessage("Branch ID must be greater than zero.");
        }
    }
}
