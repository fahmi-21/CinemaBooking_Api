using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Coupons.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Coupons.Commands.Update;

public sealed class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, ApiResponse<UpdateCouponResponse>>
{
    private readonly IAppDbContext _context;

    public UpdateCouponCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdateCouponResponse>> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Coupons.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdateCouponResponse>(false, "Coupon not found.", null);

        entity.Code = request.Code;
        entity.DiscountType = request.DiscountType;
        entity.Value = request.Value;
        entity.MinOrderAmount = request.MinOrderAmount;
        entity.MaxUsageCount = request.MaxUsageCount;
        entity.UsedCount = request.UsedCount;
        entity.ExpiryDate = request.ExpiryDate;
        entity.IsActive = request.IsActive;
        entity.ApplicableMovieId = request.ApplicableMovieId;
        entity.ApplicableBranchId = request.ApplicableBranchId;
        entity.SetUpdatedAt();
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdateCouponResponse>(
            true,
            "Coupon updated successfully.",
            new UpdateCouponResponse(entity.Id));
    }
}
