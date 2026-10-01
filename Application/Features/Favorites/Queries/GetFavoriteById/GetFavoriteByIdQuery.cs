using Application.Common.Models;
using Application.Features.Favorites.Responses;

namespace Application.Features.Favorites.Queries.GetFavoriteById;

public sealed record GetFavoriteByIdQuery(int Id) : IRequest<ApiResponse<FavoriteResponse>>;
