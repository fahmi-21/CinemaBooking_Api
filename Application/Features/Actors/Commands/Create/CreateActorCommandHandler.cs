using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Actors.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Actors.Commands.Create;

public sealed class CreateActorCommandHandler : IRequestHandler<CreateActorCommand, ApiResponse<CreateActorResponse>>
{
    private readonly IAppDbContext _context;

    public CreateActorCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreateActorResponse>> Handle(CreateActorCommand request, CancellationToken cancellationToken)
    {
        var entity = new Actor
        {
                FullName = request.FullName,
                PhotoUrl = request.PhotoUrl
        };
            entity.SetCreatedAt();
        await _context.Actors.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreateActorResponse>(
            true,
            "Actor created successfully.",
            new CreateActorResponse(entity.Id));
    }
}
