using Application.Common.Models;
using Application.Features.Payments.Responses;

namespace Application.Features.Payments.Queries.GetPayments;

public sealed record GetPaymentsQuery : IRequest<ApiResponse<GetPaymentsResponse>>;
