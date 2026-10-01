using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Showtimes.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Showtimes.Queries.GetShowtimeById;

public sealed class GetShowtimeByIdQueryHandler : IRequestHandler<GetShowtimeByIdQuery, ApiResponse<ShowtimeResponse>>
{
    private readonly IAppDbContext _context;

    public GetShowtimeByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<ShowtimeResponse>> Handle(GetShowtimeByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Showtimes
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new ShowtimeResponse(
                    e.Id,
                    e.MovieId,
                    e.HallId,
                    e.Date,
                    e.StartTime,
                    e.EndTime,
                    e.BasePrice,
                    e.Status))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<ShowtimeResponse>(false, "Showtime not found.", null)
            : new ApiResponse<ShowtimeResponse>(true, "Showtime retrieved successfully.", entity);
    }
}
