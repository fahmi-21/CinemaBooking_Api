using Application.Common.Models;
using Domain.Enums;
using Application.Features.Payments.Responses;

namespace Application.Features.Payments.Queries.GetPaymentById;

public sealed record GetPaymentByIdQuery(int Id) : IRequest<ApiResponse<PaymentResponse>>;
