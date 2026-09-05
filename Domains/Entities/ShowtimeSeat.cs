using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class ShowtimeSeat : BaseEntity
{
    public Guid ShowtimeId { get; set; }
    public Guid SeatId { get; set; }
    public ShowtimeSeatStatus Status { get; set; }
    public Guid? LockedByUserId { get; set; }
    public DateTime? LockExpiresAt { get; set; }
    public decimal Price { get; set; }
    public byte[] RowVersion { get; set; } = [];

    public Showtime Showtime { get; set; } = null!;
    public Seat Seat { get; set; } = null!;
    public ICollection<BookingSeat> BookingSeats { get; set; } = [];
}