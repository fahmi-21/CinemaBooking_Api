using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Actors.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Actors.Queries.GetActors;

public sealed class GetActorsQueryHandler : IRequestHandler<GetActorsQuery, ApiResponse<GetActorsResponse>>
{
    private readonly IAppDbContext _context;

    public GetActorsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetActorsResponse>> Handle(GetActorsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Actors
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new ActorResponse(
                    e.Id,
                    e.FullName,
                    e.PhotoUrl))
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetActorsResponse>(
            true,
            "Actors retrieved successfully.",
            new GetActorsResponse(items));
    }
}
