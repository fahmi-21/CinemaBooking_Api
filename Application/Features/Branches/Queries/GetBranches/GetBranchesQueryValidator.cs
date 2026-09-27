using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Queries.GetBranches
{
    public sealed class GetBranchesQueryValidator : AbstractValidator<GetBranchesQuery>
    {
        public GetBranchesQueryValidator ()
        {
            RuleFor(e => e.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than zero.");

            RuleFor(e => e.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be between 1 and 100.");
        }
    }
}
