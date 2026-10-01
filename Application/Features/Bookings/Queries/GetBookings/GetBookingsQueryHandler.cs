using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Bookings.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookings.Queries.GetBookings;

public sealed class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, ApiResponse<GetBookingsResponse>>
{
    private readonly IAppDbContext _context;

    public GetBookingsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetBookingsResponse>> Handle(GetBookingsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Bookings
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new BookingResponse(
                    e.Id,
                    e.UserId,
                    e.ShowtimeId,
                    e.Status,
                    e.TotalAmount,
                    e.ExpiresAt,
                    e.ConfirmedAt))
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetBookingsResponse>(
            true,
            "Bookings retrieved successfully.",
            new GetBookingsResponse(items));
    }
}
