namespace Application.Features.Favorites.Responses;

public sealed record FavoriteResponse(
    int Id,
    Guid UserId,
    int MovieId);
