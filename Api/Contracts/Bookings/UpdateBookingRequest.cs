using Domain.Enums;
namespace Api.Contracts.Bookings;

public sealed record UpdateBookingRequest(
    Guid UserId,
    int ShowtimeId,
    BookingStatus Status,
    decimal TotalAmount,
    DateTime? ExpiresAt,
    DateTime? ConfirmedAt);
