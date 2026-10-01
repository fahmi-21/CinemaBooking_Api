using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.Features.Halls.Commands.Update
{
    public sealed class UpdateHallCommandValidator : AbstractValidator< UpdateHallCommand>
    {
        public UpdateHallCommandValidator()
        {
            RuleFor(e => e.Id)
                .GreaterThan(0)
                .WithMessage( "Id Must Be Greater Than Zero");

            RuleFor(x => x.Id)
        .GreaterThan(0)
        .WithMessage("Hall ID must be greater than zero.");

            RuleFor(x => x.BranchId)
                .GreaterThan(0)
                .WithMessage("Branch ID must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage(
                    "Hall name is required and must not exceed 100 characters.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid hall type.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than zero.");

            RuleFor(x => x.CleaningBufferMinutes)
                .GreaterThanOrEqualTo(0)
                .WithMessage(
                    "Cleaning buffer minutes cannot be negative.");
        }
    }
}
