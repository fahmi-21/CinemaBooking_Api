using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Actors.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Actors.Queries.GetActorById;

public sealed class GetActorByIdQueryHandler : IRequestHandler<GetActorByIdQuery, ApiResponse<ActorResponse>>
{
    private readonly IAppDbContext _context;

    public GetActorByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<ActorResponse>> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Actors
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new ActorResponse(
                    e.Id,
                    e.FullName,
                    e.PhotoUrl))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<ActorResponse>(false, "Actor not found.", null)
            : new ApiResponse<ActorResponse>(true, "Actor retrieved successfully.", entity);
    }
}
