using Domain.Enums;
namespace Application.Features.Coupons.Responses;

public sealed record CouponResponse(
    int Id,
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
