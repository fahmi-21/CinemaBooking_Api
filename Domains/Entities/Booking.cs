using Domain.Entities.Common;
using Domain.Entities.Identity;
using Domain.Enums;

namespace Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int ShowtimeId { get; set; }
    public BookingStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime? ConfirmedAt { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Showtime Showtime { get; set; } = null!;
    public ICollection<BookingSeat> BookingSeats { get; set; } = [];
    public ICollection<Payment> Payments { get; set; } = [];
    public Ticket? Ticket { get; set; }
    public CouponRedemption? CouponRedemption { get; set; }
}