using Domain.Enums;
namespace Api.Contracts.Payments;

public sealed record UpdatePaymentRequest(
    int BookingId,
    decimal Amount,
    PaymentMethod Method,
    PaymentStatus Status,
    string IdempotencyKey,
    string? ProviderReference,
    DateTime? ProcessedAt);
