using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Payments.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.Commands.Create;

public sealed class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ApiResponse<CreatePaymentResponse>>
{
    private readonly IAppDbContext _context;

    public CreatePaymentCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreatePaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Payment
        {
                BookingId = request.BookingId,
                Amount = request.Amount,
                Method = request.Method,
                Status = request.Status,
                IdempotencyKey = request.IdempotencyKey,
                ProviderReference = request.ProviderReference,
                ProcessedAt = request.ProcessedAt
        };
            entity.SetCreatedAt();
        await _context.Payments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreatePaymentResponse>(
            true,
            "Payment created successfully.",
            new CreatePaymentResponse(entity.Id));
    }
}
