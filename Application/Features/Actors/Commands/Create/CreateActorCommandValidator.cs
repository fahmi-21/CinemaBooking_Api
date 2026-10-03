
namespace Application.Features.Actors.Commands.Create;

public sealed class CreateActorCommandValidator : AbstractValidator<CreateActorCommand>
{
    public CreateActorCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(100)
            .WithMessage("Full name must not exceed 100 characters.");

        RuleFor(x => x.PhotoUrl)
            .MaximumLength(500)
            .WithMessage("Photo URL must not exceed 500 characters.");
    }
}