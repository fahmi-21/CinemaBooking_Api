using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Showtimes.Commands.Delete;

public sealed class DeleteShowtimeCommandHandler : IRequestHandler<DeleteShowtimeCommand, ApiResponse<EmptyResponse?>>
{
    private readonly IAppDbContext _context;

    public DeleteShowtimeCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteShowtimeCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Showtimes.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<EmptyResponse?>(false, "Showtime not found.", null);

        _context.Showtimes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResponse<EmptyResponse?>(true, "Showtime deleted successfully.", null);
    }
}
