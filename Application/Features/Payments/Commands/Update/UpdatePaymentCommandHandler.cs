using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Payments.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.Commands.Update;

public sealed class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, ApiResponse<UpdatePaymentResponse>>
{
    private readonly IAppDbContext _context;

    public UpdatePaymentCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdatePaymentResponse>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Payments.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdatePaymentResponse>(false, "Payment not found.", null);

        entity.BookingId = request.BookingId;
        entity.Amount = request.Amount;
        entity.Method = request.Method;
        entity.Status = request.Status;
        entity.IdempotencyKey = request.IdempotencyKey;
        entity.ProviderReference = request.ProviderReference;
        entity.ProcessedAt = request.ProcessedAt;
        entity.SetUpdatedAt();
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdatePaymentResponse>(
            true,
            "Payment updated successfully.",
            new UpdatePaymentResponse(entity.Id));
    }
}
