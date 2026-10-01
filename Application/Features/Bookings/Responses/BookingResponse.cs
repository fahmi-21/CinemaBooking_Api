using Domain.Enums;
namespace Application.Features.Bookings.Responses;

public sealed record BookingResponse(
    int Id,
    Guid UserId,
    int ShowtimeId,
    BookingStatus Status,
    decimal TotalAmount,
    DateTime? ExpiresAt,
    DateTime? ConfirmedAt);
