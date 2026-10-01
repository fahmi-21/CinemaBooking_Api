using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Favorites.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Favorites.Queries.GetFavoriteById;

public sealed class GetFavoriteByIdQueryHandler : IRequestHandler<GetFavoriteByIdQuery, ApiResponse<FavoriteResponse>>
{
    private readonly IAppDbContext _context;

    public GetFavoriteByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<FavoriteResponse>> Handle(GetFavoriteByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Favorites
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new FavoriteResponse(
                    e.Id,
                    e.UserId,
                    e.MovieId))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<FavoriteResponse>(false, "Favorite not found.", null)
            : new ApiResponse<FavoriteResponse>(true, "Favorite retrieved successfully.", entity);
    }
}
