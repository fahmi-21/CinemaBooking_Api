using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Movies.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Movies.Queries.GetMovies;

public sealed class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, ApiResponse<GetMoviesResponse>>
{
    private readonly IAppDbContext _context;

    public GetMoviesQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetMoviesResponse>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Movies
            .AsNoTracking()
            .OrderBy(e => e.Id)
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
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetMoviesResponse>(
            true,
            "Movies retrieved successfully.",
            new GetMoviesResponse(items));
    }
}
