using Application.Common.Models;
using Domain.Enums;
using Application.Features.Movies.Responses;

namespace Application.Features.Movies.Queries.GetMovieById;

public sealed record GetMovieByIdQuery(int Id) : IRequest<ApiResponse<MovieResponse>>;
