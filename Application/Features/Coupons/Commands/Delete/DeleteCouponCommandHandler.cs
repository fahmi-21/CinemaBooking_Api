using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Coupons.Commands.Delete;

public sealed class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, ApiResponse<EmptyResponse?>>
{
    private readonly IAppDbContext _context;

    public DeleteCouponCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Coupons.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<EmptyResponse?>(false, "Coupon not found.", null);

        _context.Coupons.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResponse<EmptyResponse?>(true, "Coupon deleted successfully.", null);
    }
}
