using Application.Common.Models;
using Application.Features.Favorites.Responses;

namespace Application.Features.Favorites.Queries.GetFavorites;

public sealed record GetFavoritesQuery : IRequest<ApiResponse<GetFavoritesResponse>>;
