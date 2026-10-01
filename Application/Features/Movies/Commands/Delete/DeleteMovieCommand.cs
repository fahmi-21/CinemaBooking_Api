using Application.Common.Models;

namespace Application.Features.Movies.Commands.Delete;

public sealed record DeleteMovieCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
