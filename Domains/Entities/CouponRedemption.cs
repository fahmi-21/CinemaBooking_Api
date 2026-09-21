using Domain.Entities.Identity;

namespace Domain.Entities;

public class CouponRedemption
{
    public int Id { get; set; }
    public int CouponId { get; set; }
    public Guid UserId { get; set; }
    public int BookingId { get; set; }
    public DateTime RedeemedAt { get; set; }

    public Coupon Coupon { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public Booking Booking { get; set; } = null!;
}