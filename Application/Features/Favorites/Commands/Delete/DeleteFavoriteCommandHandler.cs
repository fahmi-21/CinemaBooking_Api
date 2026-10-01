using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Favorites.Commands.Delete;

public sealed class DeleteFavoriteCommandHandler : IRequestHandler<DeleteFavoriteCommand, ApiResponse<EmptyResponse?>>
{
    private readonly IAppDbContext _context;

    public DeleteFavoriteCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Favorites.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<EmptyResponse?>(false, "Favorite not found.", null);

        _context.Favorites.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResponse<EmptyResponse?>(true, "Favorite deleted successfully.", null);
    }
}
