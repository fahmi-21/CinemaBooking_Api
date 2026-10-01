using System.Collections.Generic;

namespace Application.Features.Payments.Responses;

public sealed record GetPaymentsResponse(IReadOnlyList<PaymentResponse> Items);
