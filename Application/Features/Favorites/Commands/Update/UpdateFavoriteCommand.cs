using Application.Features.Favorites.Responses;
using Application.Common.Models;
namespace Application.Features.Favorites.Commands.Update;

public sealed record UpdateFavoriteCommand(
    int Id,
    Guid UserId,
    int MovieId) : IRequest<ApiResponse<UpdateFavoriteResponse>>;
