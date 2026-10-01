using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Coupons.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Coupons.Commands.Create;

public sealed class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, ApiResponse<CreateCouponResponse>>
{
    private readonly IAppDbContext _context;

    public CreateCouponCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreateCouponResponse>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var entity = new Coupon
        {
                Code = request.Code,
                DiscountType = request.DiscountType,
                Value = request.Value,
                MinOrderAmount = request.MinOrderAmount,
                MaxUsageCount = request.MaxUsageCount,
                UsedCount = request.UsedCount,
                ExpiryDate = request.ExpiryDate,
                IsActive = request.IsActive,
                ApplicableMovieId = request.ApplicableMovieId,
                ApplicableBranchId = request.ApplicableBranchId
        };
            entity.SetCreatedAt();
        await _context.Coupons.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreateCouponResponse>(
            true,
            "Coupon created successfully.",
            new CreateCouponResponse(entity.Id));
    }
}
