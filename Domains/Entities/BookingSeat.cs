namespace Domain.Entities;

public class BookingSeat
{
    public int Id { get; set; }
    public int BookingId { get; set; }
    public int ShowtimeSeatId { get; set; }
    public decimal PriceAtBooking { get; set; }

    public Booking Booking { get; set; } = null!;
    public ShowtimeSeat ShowtimeSeat { get; set; } = null!;
}