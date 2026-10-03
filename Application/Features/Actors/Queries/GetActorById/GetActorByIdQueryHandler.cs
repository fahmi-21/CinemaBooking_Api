using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Actors.Queries.GetActorById;

public sealed class GetActorByIdQueryHandler : IRequestHandler<GetActorByIdQuery, ApiResponse<GetActorByIdResponse>>
{
    private readonly IAppDbContext _context;

    public GetActorByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetActorByIdResponse>> Handle(GetActorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Actors
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new GetActorByIdResponse(
                    e.Id,
                    e.FullName,
                    e.PhotoUrl))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<GetActorByIdResponse>(false, "Actor not found.", null)
            : new ApiResponse<GetActorByIdResponse>(true, "Actor retrieved successfully.", entity);
    }
}
