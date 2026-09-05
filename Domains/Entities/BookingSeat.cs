namespace Domain.Entities;

public class BookingSeat
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid ShowtimeSeatId { get; set; }
    public decimal PriceAtBooking { get; set; }

    public Booking Booking { get; set; } = null!;
    public ShowtimeSeat ShowtimeSeat { get; set; } = null!;
}