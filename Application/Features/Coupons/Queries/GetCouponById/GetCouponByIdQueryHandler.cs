using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Coupons.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Coupons.Queries.GetCouponById;

public sealed class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, ApiResponse<CouponResponse>>
{
    private readonly IAppDbContext _context;

    public GetCouponByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CouponResponse>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Coupons
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new CouponResponse(
                    e.Id,
                    e.Code,
                    e.DiscountType,
                    e.Value,
                    e.MinOrderAmount,
                    e.MaxUsageCount,
                    e.UsedCount,
                    e.ExpiryDate,
                    e.IsActive,
                    e.ApplicableMovieId,
                    e.ApplicableBranchId))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<CouponResponse>(false, "Coupon not found.", null)
            : new ApiResponse<CouponResponse>(true, "Coupon retrieved successfully.", entity);
    }
}
