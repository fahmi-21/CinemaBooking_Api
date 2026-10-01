using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Movies.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Movies.Commands.Create;

public sealed class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, ApiResponse<CreateMovieResponse>>
{
    private readonly IAppDbContext _context;

    public CreateMovieCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreateMovieResponse>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
    {
        var entity = new Movie
        {
                Title = request.Title,
                Description = request.Description,
                DurationMinutes = request.DurationMinutes,
                ReleaseDate = request.ReleaseDate,
                PosterUrl = request.PosterUrl,
                TrailerUrl = request.TrailerUrl,
                Language = request.Language,
                Country = request.Country,
                Director = request.Director,
                AgeRating = request.AgeRating,
                AverageRating = request.AverageRating,
                Status = request.Status
        };
            entity.SetCreatedAt();
        await _context.Movies.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreateMovieResponse>(
            true,
            "Movie created successfully.",
            new CreateMovieResponse(entity.Id));
    }
}
