using Application.Features.Coupons.Responses;
using Application.Common.Models;
using Domain.Enums;
namespace Application.Features.Coupons.Commands.Create;

public sealed record CreateCouponCommand(
    string Code,
    CouponDiscountType DiscountType,
    decimal Value,
    decimal? MinOrderAmount,
    int? MaxUsageCount,
    int UsedCount,
    DateTime ExpiryDate,
    bool IsActive,
    int? ApplicableMovieId,
    int? ApplicableBranchId) : IRequest<ApiResponse<CreateCouponResponse>>;
