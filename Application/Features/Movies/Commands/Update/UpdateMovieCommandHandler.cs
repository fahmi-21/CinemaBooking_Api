using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Movies.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Movies.Commands.Update;

public sealed class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, ApiResponse<UpdateMovieResponse>>
{
    private readonly IAppDbContext _context;

    public UpdateMovieCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdateMovieResponse>> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movies.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdateMovieResponse>(false, "Movie not found.", null);

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.DurationMinutes = request.DurationMinutes;
        entity.ReleaseDate = request.ReleaseDate;
        entity.PosterUrl = request.PosterUrl;
        entity.TrailerUrl = request.TrailerUrl;
        entity.Language = request.Language;
        entity.Country = request.Country;
        entity.Director = request.Director;
        entity.AgeRating = request.AgeRating;
        entity.AverageRating = request.AverageRating;
        entity.Status = request.Status;
        entity.SetUpdatedAt();
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdateMovieResponse>(
            true,
            "Movie updated successfully.",
            new UpdateMovieResponse(entity.Id));
    }
}
