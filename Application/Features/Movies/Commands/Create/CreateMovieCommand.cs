using Application.Features.Movies.Responses;
using Application.Common.Models;
using Domain.Enums;
namespace Application.Features.Movies.Commands.Create;

public sealed record CreateMovieCommand(
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
    MovieStatus Status) : IRequest<ApiResponse<CreateMovieResponse>>;
