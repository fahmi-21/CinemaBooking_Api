using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Payments.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.Queries.GetPayments;

public sealed class GetPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, ApiResponse<GetPaymentsResponse>>
{
    private readonly IAppDbContext _context;

    public GetPaymentsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetPaymentsResponse>> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Payments
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new PaymentResponse(
                    e.Id,
                    e.BookingId,
                    e.Amount,
                    e.Method,
                    e.Status,
                    e.ProcessedAt))
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetPaymentsResponse>(
            true,
            "Payments retrieved successfully.",
            new GetPaymentsResponse(items));
    }
}
