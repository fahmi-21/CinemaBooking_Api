using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Movies.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Movies.Queries.GetMovieById;

public sealed class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, ApiResponse<MovieResponse>>
{
    private readonly IAppDbContext _context;

    public GetMovieByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<MovieResponse>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Movies
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new MovieResponse(
                    e.Id,
                    e.Title,
                    e.Description,
                    e.DurationMinutes,
                    e.ReleaseDate,
                    e.PosterUrl,
                    e.TrailerUrl,
                    e.Language,
                    e.Country,
                    e.Director,
                    e.AgeRating,
                    e.AverageRating,
                    e.Status))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<MovieResponse>(false, "Movie not found.", null)
            : new ApiResponse<MovieResponse>(true, "Movie retrieved successfully.", entity);
    }
}
