using System.Collections.Generic;

namespace Application.Features.Favorites.Responses;

public sealed record GetFavoritesResponse(IReadOnlyList<FavoriteResponse> Items);
