using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Movies.Commands.Delete;

public sealed class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, ApiResponse<EmptyResponse?>>
{
    private readonly IAppDbContext _context;

    public DeleteMovieCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movies.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<EmptyResponse?>(false, "Movie not found.", null);

        _context.Movies.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResponse<EmptyResponse?>(true, "Movie deleted successfully.", null);
    }
}
