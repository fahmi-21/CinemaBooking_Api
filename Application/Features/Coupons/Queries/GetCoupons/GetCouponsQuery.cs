using Application.Common.Models;
using Application.Features.Coupons.Responses;

namespace Application.Features.Coupons.Queries.GetCoupons;

public sealed record GetCouponsQuery : IRequest<ApiResponse<GetCouponsResponse>>;
