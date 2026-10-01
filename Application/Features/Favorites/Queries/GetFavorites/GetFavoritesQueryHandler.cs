using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Favorites.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Favorites.Queries.GetFavorites;

public sealed class GetFavoritesQueryHandler : IRequestHandler<GetFavoritesQuery, ApiResponse<GetFavoritesResponse>>
{
    private readonly IAppDbContext _context;

    public GetFavoritesQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetFavoritesResponse>> Handle(GetFavoritesQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Favorites
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new FavoriteResponse(
                    e.Id,
                    e.UserId,
                    e.MovieId))
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetFavoritesResponse>(
            true,
            "Favorites retrieved successfully.",
            new GetFavoritesResponse(items));
    }
}
