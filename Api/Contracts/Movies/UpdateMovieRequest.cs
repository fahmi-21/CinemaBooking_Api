using Domain.Enums;
namespace Api.Contracts.Movies;

public sealed record UpdateMovieRequest(
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
