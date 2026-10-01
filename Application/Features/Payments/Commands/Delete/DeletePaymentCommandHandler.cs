using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Payments.Commands.Delete;

public sealed class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, ApiResponse<EmptyResponse?>>
{
    private readonly IAppDbContext _context;

    public DeletePaymentCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<EmptyResponse?>> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Payments.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<EmptyResponse?>(false, "Payment not found.", null);

        _context.Payments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResponse<EmptyResponse?>(true, "Payment deleted successfully.", null);
    }
}
