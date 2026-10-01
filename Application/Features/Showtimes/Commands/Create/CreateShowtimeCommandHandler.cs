using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Showtimes.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Showtimes.Commands.Create;

public sealed class CreateShowtimeCommandHandler : IRequestHandler<CreateShowtimeCommand, ApiResponse<CreateShowtimeResponse>>
{
    private readonly IAppDbContext _context;

    public CreateShowtimeCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreateShowtimeResponse>> Handle(CreateShowtimeCommand request, CancellationToken cancellationToken)
    {
        var entity = new Showtime
        {
                MovieId = request.MovieId,
                HallId = request.HallId,
                Date = request.Date,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                BasePrice = request.BasePrice,
                Status = request.Status
        };
            entity.SetCreatedAt();
        await _context.Showtimes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreateShowtimeResponse>(
            true,
            "Showtime created successfully.",
            new CreateShowtimeResponse(entity.Id));
    }
}
