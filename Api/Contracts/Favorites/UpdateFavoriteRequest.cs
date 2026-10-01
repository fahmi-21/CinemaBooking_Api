namespace Api.Contracts.Favorites;

public sealed record UpdateFavoriteRequest(
    Guid UserId,
    int MovieId);
