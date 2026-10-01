using Application.Common.Models;

namespace Application.Features.Favorites.Commands.Delete;

public sealed record DeleteFavoriteCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
