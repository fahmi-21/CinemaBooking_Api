using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Seat : BaseEntity
{
    public int SectionId { get; set; }
    public string RowLabel { get; set; } = string.Empty;
    public int SeatNumber { get; set; }
    public SeatType SeatType { get; set; }

    public Section Section { get; set; } = null!;
    public ICollection<ShowtimeSeat> ShowtimeSeats { get; set; } = [];
}