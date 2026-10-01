using Application.Common.Models;
using Domain.Enums;
using Application.Features.Coupons.Responses;

namespace Application.Features.Coupons.Queries.GetCouponById;

public sealed record GetCouponByIdQuery(int Id) : IRequest<ApiResponse<CouponResponse>>;
