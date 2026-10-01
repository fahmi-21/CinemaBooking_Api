using Application.Features.Bookings.Responses;
using Application.Common.Models;
using Domain.Enums;
namespace Application.Features.Bookings.Commands.Update;

public sealed record UpdateBookingCommand(
    int Id,
    Guid UserId,
    int ShowtimeId,
    BookingStatus Status,
    decimal TotalAmount,
    DateTime? ExpiresAt,
    DateTime? ConfirmedAt) : IRequest<ApiResponse<UpdateBookingResponse>>;
