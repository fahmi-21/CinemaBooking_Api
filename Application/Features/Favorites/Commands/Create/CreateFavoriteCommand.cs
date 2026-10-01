using Application.Features.Favorites.Responses;
using Application.Common.Models;
namespace Application.Features.Favorites.Commands.Create;

public sealed record CreateFavoriteCommand(
    Guid UserId,
    int MovieId) : IRequest<ApiResponse<CreateFavoriteResponse>>;
