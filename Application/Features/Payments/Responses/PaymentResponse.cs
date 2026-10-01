using Domain.Enums;
namespace Application.Features.Payments.Responses;

public sealed record PaymentResponse(
    int Id,
    int BookingId,
    decimal Amount,
    PaymentMethod Method,
    PaymentStatus Status,
    DateTime? ProcessedAt);
