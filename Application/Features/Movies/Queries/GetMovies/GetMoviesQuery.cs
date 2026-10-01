using Application.Common.Models;
using Application.Features.Movies.Responses;

namespace Application.Features.Movies.Queries.GetMovies;

public sealed record GetMoviesQuery : IRequest<ApiResponse<GetMoviesResponse>>;
