using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookings.Commands.Delete;

public sealed class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, ApiResponse<EmptyResponse?>>
{
    private readonly IAppDbContext _context;

    public DeleteBookingCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bookings.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<EmptyResponse?>(false, "Booking not found.", null);

        _context.Bookings.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResponse<EmptyResponse?>(true, "Booking deleted successfully.", null);
    }
}
