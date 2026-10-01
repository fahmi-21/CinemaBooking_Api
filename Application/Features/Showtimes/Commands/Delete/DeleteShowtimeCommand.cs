using Application.Common.Models;

namespace Application.Features.Showtimes.Commands.Delete;

public sealed record DeleteShowtimeCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
