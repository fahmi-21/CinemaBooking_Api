using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Coupon : BaseEntity
{
    public string Code { get; set; } = string.Empty;
    public CouponDiscountType DiscountType { get; set; }
    public decimal Value { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public int? MaxUsageCount { get; set; }
    public int UsedCount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
    public Guid? ApplicableMovieId { get; set; }
    public Guid? ApplicableBranchId { get; set; }

    public Movie? ApplicableMovie { get; set; }
    public Branch? ApplicableBranch { get; set; }
    public ICollection<CouponRedemption> CouponRedemptions { get; set; } = [];
}