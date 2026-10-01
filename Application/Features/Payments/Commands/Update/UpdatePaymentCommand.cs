using Application.Features.Payments.Responses;
using Application.Common.Models;
using Domain.Enums;
namespace Application.Features.Payments.Commands.Update;

public sealed record UpdatePaymentCommand(
    int Id,
    int BookingId,
    decimal Amount,
    PaymentMethod Method,
    PaymentStatus Status,
    string IdempotencyKey,
    string? ProviderReference,
    DateTime? ProcessedAt) : IRequest<ApiResponse<UpdatePaymentResponse>>;
