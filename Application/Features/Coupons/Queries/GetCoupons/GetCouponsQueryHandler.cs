using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Coupons.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Coupons.Queries.GetCoupons;

public sealed class GetCouponsQueryHandler : IRequestHandler<GetCouponsQuery, ApiResponse<GetCouponsResponse>>
{
    private readonly IAppDbContext _context;

    public GetCouponsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetCouponsResponse>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Coupons
            .AsNoTracking()
            .OrderBy(e => e.Id)
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
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetCouponsResponse>(
            true,
            "Coupons retrieved successfully.",
            new GetCouponsResponse(items));
    }
}
