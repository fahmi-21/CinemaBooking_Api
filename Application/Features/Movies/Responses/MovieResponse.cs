using Domain.Enums;
namespace Application.Features.Movies.Responses;

public sealed record MovieResponse(
    int Id,
    string Title,
    string? Description,
    int DurationMinutes,
    DateTime? ReleaseDate,
    string PosterUrl,
    string TrailerUrl,
    string Language,
    string Country,
    string Director,
    AgeRating AgeRating,
    decimal? AverageRating,
    MovieStatus Status);
