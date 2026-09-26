using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Update
{
    public sealed class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Branch ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Branch name is required and must not exceed 100 characters.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(250)
                .WithMessage("Branch address is required and must not exceed 250 characters.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90)
                .When(x => x.Latitude.HasValue)
                .WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180)
                .When(x => x.Longitude.HasValue)
                .WithMessage("Longitude must be between -180 and 180.");

            RuleFor(x => x.GoogleMapsUrl)
                .MaximumLength(500)
                .When(x => !string.IsNullOrWhiteSpace(x.GoogleMapsUrl));
        }
    }
}
