namespace Api.Contracts.Bookings;

public sealed record CreateBookingRequest(
    int ShowtimeId,
    IReadOnlyCollection<int>? ShowtimeSeatIds);
