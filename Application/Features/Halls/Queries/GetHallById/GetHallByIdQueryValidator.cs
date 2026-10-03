using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHallById
{
    public sealed class GetHallByIdQueryValidator : AbstractValidator<GetHallByIdQuery>
    {
        public GetHallByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Hall ID is required.");
        }
    }
}
