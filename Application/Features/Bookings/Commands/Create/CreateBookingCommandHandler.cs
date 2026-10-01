using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Bookings.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bookings.Commands.Create;

public sealed class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ApiResponse<CreateBookingResponse>>
{
    private readonly IAppDbContext _context;

    public CreateBookingCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreateBookingResponse>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var entity = new Booking
        {
                UserId = request.UserId,
                ShowtimeId = request.ShowtimeId,
                Status = request.Status,
                TotalAmount = request.TotalAmount,
                ExpiresAt = request.ExpiresAt,
                ConfirmedAt = request.ConfirmedAt
        };
        await _context.Bookings.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreateBookingResponse>(
            true,
            "Booking created successfully.",
            new CreateBookingResponse(entity.Id));
    }
}
