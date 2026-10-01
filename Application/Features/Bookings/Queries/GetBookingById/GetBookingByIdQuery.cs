using Application.Common.Models;
using Domain.Enums;
using Application.Features.Bookings.Responses;

namespace Application.Features.Bookings.Queries.GetBookingById;

public sealed record GetBookingByIdQuery(int Id) : IRequest<ApiResponse<BookingResponse>>;
