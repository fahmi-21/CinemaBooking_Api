using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Actors;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Actors.Queries.GetActors;

public sealed class GetActorsQueryHandler : IRequestHandler<GetActorsQuery, ApiResponse<GetActorsResponse>>
{
    private readonly IAppDbContext _context;

    public GetActorsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetActorsResponse>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Actors.AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

        var actors = await query
            .OrderBy(e => e.Id)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new ActorItemResponse(
                               x.Id,
                               x.FullName,
                               x.PhotoUrl
                           ))
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetActorsResponse>(
            true,
            "Actors retrieved successfully.",
            new GetActorsResponse(actors, totalCount, request.PageNumber, request.PageSize, totalPages));
    }
}
