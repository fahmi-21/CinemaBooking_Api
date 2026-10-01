using System.Collections.Generic;

namespace Application.Features.Movies.Responses;

public sealed record GetMoviesResponse(IReadOnlyList<MovieResponse> Items);
