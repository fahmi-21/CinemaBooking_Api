using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Bookings.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookings.Queries.GetBookingById;

public sealed class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, ApiResponse<BookingResponse>>
{
    private readonly IAppDbContext _context;

    public GetBookingByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<BookingResponse>> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bookings
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new BookingResponse(
                    e.Id,
                    e.UserId,
                    e.ShowtimeId,
                    e.Status,
                    e.TotalAmount,
                    e.ExpiresAt,
                    e.ConfirmedAt))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<BookingResponse>(false, "Booking not found.", null)
            : new ApiResponse<BookingResponse>(true, "Booking retrieved successfully.", entity);
    }
}
