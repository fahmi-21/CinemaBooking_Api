using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Showtimes.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Showtimes.Commands.Update;

public sealed class UpdateShowtimeCommandHandler : IRequestHandler<UpdateShowtimeCommand, ApiResponse<UpdateShowtimeResponse>>
{
    private readonly IAppDbContext _context;

    public UpdateShowtimeCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdateShowtimeResponse>> Handle(UpdateShowtimeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Showtimes.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdateShowtimeResponse>(false, "Showtime not found.", null);

        entity.MovieId = request.MovieId;
        entity.HallId = request.HallId;
        entity.Date = request.Date;
        entity.StartTime = request.StartTime;
        entity.EndTime = request.EndTime;
        entity.BasePrice = request.BasePrice;
        entity.Status = request.Status;
        entity.SetUpdatedAt();
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdateShowtimeResponse>(
            true,
            "Showtime updated successfully.",
            new UpdateShowtimeResponse(entity.Id));
    }
}
