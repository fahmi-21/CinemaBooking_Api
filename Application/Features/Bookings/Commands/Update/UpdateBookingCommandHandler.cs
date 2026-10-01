using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Bookings.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookings.Commands.Update;

public sealed class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, ApiResponse<UpdateBookingResponse>>
{
    private readonly IAppDbContext _context;

    public UpdateBookingCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdateBookingResponse>> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Bookings.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdateBookingResponse>(false, "Booking not found.", null);

        entity.UserId = request.UserId;
        entity.ShowtimeId = request.ShowtimeId;
        entity.Status = request.Status;
        entity.TotalAmount = request.TotalAmount;
        entity.ExpiresAt = request.ExpiresAt;
        entity.ConfirmedAt = request.ConfirmedAt;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdateBookingResponse>(
            true,
            "Booking updated successfully.",
            new UpdateBookingResponse(entity.Id));
    }
}
