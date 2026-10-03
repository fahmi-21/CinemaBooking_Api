using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHalls
{
    public sealed class GetHallsQueryValidator : AbstractValidator<GetHallsQuery>
    {
        public GetHallsQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.");
        }
    }
}
