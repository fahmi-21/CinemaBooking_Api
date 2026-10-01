using Application.Common.Models;

namespace Application.Features.Coupons.Commands.Delete;

public sealed record DeleteCouponCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
