using System.Collections.Generic;

namespace Application.Features.Bookings.Responses;

public sealed record GetBookingsResponse(IReadOnlyList<BookingResponse> Items);
