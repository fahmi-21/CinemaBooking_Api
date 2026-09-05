using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Showtime : BaseEntity
{
    public Guid MovieId { get; set; }
    public Guid HallId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal BasePrice { get; set; }
    public ShowtimeStatus Status { get; set; }

    public Movie Movie { get; set; } = null!;
    public Hall Hall { get; set; } = null!;
    public ICollection<ShowtimeSeat> ShowtimeSeats { get; set; } = [];
}