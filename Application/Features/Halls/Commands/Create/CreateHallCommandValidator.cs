using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Application.Features.Halls.Commands.Create
{
    public sealed class CreateHallCommandValidator : AbstractValidator<CreateHallCommand>
    {
        public CreateHallCommandValidator ()
        {
            RuleFor(x => x.BranchId)
            .GreaterThan(0)
            .WithMessage("Branch ID must be greater than zero.");

            RuleFor(e => e.Name)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100)
                .WithMessage("Hall name is required and must not exceed 100 characters.");

            RuleFor( e => e.Type )
                 .IsInEnum()
                 .WithMessage("Invalid hall type.");

            RuleFor(e => e.Capacity)
                .GreaterThan(0)
                .WithMessage("Capacity must be greater than zero.");

            RuleFor(x => x.CleaningBufferMinutes)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Cleaning buffer minutes cannot be negative.");

        }
    }
}
