using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Favorites.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Favorites.Commands.Create;

public sealed class CreateFavoriteCommandHandler : IRequestHandler<CreateFavoriteCommand, ApiResponse<CreateFavoriteResponse>>
{
    private readonly IAppDbContext _context;

    public CreateFavoriteCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreateFavoriteResponse>> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
    {
        var entity = new Favorite
        {
                UserId = request.UserId,
                MovieId = request.MovieId
        };
        await _context.Favorites.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreateFavoriteResponse>(
            true,
            "Favorite created successfully.",
            new CreateFavoriteResponse(entity.Id));
    }
}
