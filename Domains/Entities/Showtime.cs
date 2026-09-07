using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Showtime : BaseEntity
{
    public int MovieId { get; set; }
    public int HallId { get; set; }
    public DateOnly Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal BasePrice { get; set; }
    public ShowtimeStatus Status { get; set; }
    public Movie Movie { get; set; } = null!;
    public Hall Hall { get; set; } = null!;
    public ICollection<ShowtimeSeat> ShowtimeSeats { get; set; } = [];
    public ICollection<Booking> Bookings { get; set; } = [];
}