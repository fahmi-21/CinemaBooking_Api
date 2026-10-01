using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Showtimes.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Showtimes.Queries.GetShowtimes;

public sealed class GetShowtimesQueryHandler : IRequestHandler<GetShowtimesQuery, ApiResponse<GetShowtimesResponse>>
{
    private readonly IAppDbContext _context;

    public GetShowtimesQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetShowtimesResponse>> Handle(GetShowtimesQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Showtimes
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new ShowtimeResponse(
                    e.Id,
                    e.MovieId,
                    e.HallId,
                    e.Date,
                    e.StartTime,
                    e.EndTime,
                    e.BasePrice,
                    e.Status))
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetShowtimesResponse>(
            true,
            "Showtimes retrieved successfully.",
            new GetShowtimesResponse(items));
    }
}
