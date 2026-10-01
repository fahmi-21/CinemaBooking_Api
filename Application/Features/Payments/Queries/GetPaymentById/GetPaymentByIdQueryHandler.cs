using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Payments.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.Queries.GetPaymentById;

public sealed class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>
{
    private readonly IAppDbContext _context;

    public GetPaymentByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Payments
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new PaymentResponse(
                    e.Id,
                    e.BookingId,
                    e.Amount,
                    e.Method,
                    e.Status,
                    e.ProcessedAt))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<PaymentResponse>(false, "Payment not found.", null)
            : new ApiResponse<PaymentResponse>(true, "Payment retrieved successfully.", entity);
    }
}
