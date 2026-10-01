using System.Collections.Generic;

namespace Application.Features.Coupons.Responses;

public sealed record GetCouponsResponse(IReadOnlyList<CouponResponse> Items);
