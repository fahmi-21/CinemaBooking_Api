namespace Api.Contracts.Bookings;

public sealed record UpdateBookingRequest(Domain.Enums.BookingStatus Status);
