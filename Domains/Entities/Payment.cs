using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Payment : BaseEntity
{
    public int BookingId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; }
    public string IdempotencyKey { get; set; } = string.Empty;
    public string? ProviderReference { get; set; }
    public DateTime? ProcessedAt { get; set; }

    public Booking Booking { get; set; } = null!;
}