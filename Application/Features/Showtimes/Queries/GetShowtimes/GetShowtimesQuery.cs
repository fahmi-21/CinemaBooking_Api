using Application.Common.Models;
using Application.Features.Showtimes.Responses;

namespace Application.Features.Showtimes.Queries.GetShowtimes;

public sealed record GetShowtimesQuery : IRequest<ApiResponse<GetShowtimesResponse>>;
