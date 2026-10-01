using Application.Common.Models;
using Domain.Enums;
using Application.Features.Showtimes.Responses;

namespace Application.Features.Showtimes.Queries.GetShowtimeById;

public sealed record GetShowtimeByIdQuery(int Id) : IRequest<ApiResponse<ShowtimeResponse>>;
