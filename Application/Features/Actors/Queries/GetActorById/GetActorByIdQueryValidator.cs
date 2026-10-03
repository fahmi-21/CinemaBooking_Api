using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Actors.Queries.GetActorById
{
    public sealed class GetActorByIdQueryValidator : AbstractValidator<GetActorByIdQuery>
    {
        public GetActorByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Actor ID is required.");
        }
    }
}
