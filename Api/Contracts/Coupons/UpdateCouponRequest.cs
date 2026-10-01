using Domain.Enums;
namespace Api.Contracts.Coupons;

public sealed record UpdateCouponRequest(
    string Code,
    CouponDiscountType DiscountType,
    decimal Value,
    decimal? MinOrderAmount,
    int? MaxUsageCount,
    int UsedCount,
    DateTime ExpiryDate,
    bool IsActive,
    int? ApplicableMovieId,
    int? ApplicableBranchId);
