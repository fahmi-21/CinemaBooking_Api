using Application.Common.Models;
using Application.Features.Bookings.Responses;

namespace Application.Features.Bookings.Queries.GetBookings;

public sealed record GetBookingsQuery : IRequest<ApiResponse<GetBookingsResponse>>;
