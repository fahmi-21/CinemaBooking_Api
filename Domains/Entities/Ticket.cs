using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Ticket : BaseEntity
{
    public string TicketNumber { get; set; } = string.Empty;
    public string? QrCodePayload { get; set; }
    public TicketStatus Status { get; set; }
    public DateTime IssuedAt { get; set; }
    public DateTime? UsedAt { get; set; }
    public Guid BookingId { get; set; }

    public Booking Booking { get; set; } = null!;
}