using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Favorites.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Favorites.Commands.Update;

public sealed class UpdateFavoriteCommandHandler : IRequestHandler<UpdateFavoriteCommand, ApiResponse<UpdateFavoriteResponse>>
{
    private readonly IAppDbContext _context;

    public UpdateFavoriteCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdateFavoriteResponse>> Handle(UpdateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Favorites.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdateFavoriteResponse>(false, "Favorite not found.", null);

        entity.UserId = request.UserId;
        entity.MovieId = request.MovieId;
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdateFavoriteResponse>(
            true,
            "Favorite updated successfully.",
            new UpdateFavoriteResponse(entity.Id));
    }
}
