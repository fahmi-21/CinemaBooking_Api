using Application.Common.Models;

namespace Application.Features.Payments.Commands.Delete;

public sealed record DeletePaymentCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
