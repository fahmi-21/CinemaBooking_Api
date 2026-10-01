using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Actors.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Actors.Commands.Update;

public sealed class UpdateActorCommandHandler : IRequestHandler<UpdateActorCommand, ApiResponse<UpdateActorResponse>>
{
    private readonly IAppDbContext _context;

    public UpdateActorCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdateActorResponse>> Handle(UpdateActorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Actors.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdateActorResponse>(false, "Actor not found.", null);

        entity.FullName = request.FullName;
        entity.PhotoUrl = request.PhotoUrl;
        entity.SetUpdatedAt();
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdateActorResponse>(
            true,
            "Actor updated successfully.",
            new UpdateActorResponse(entity.Id));
    }
}
