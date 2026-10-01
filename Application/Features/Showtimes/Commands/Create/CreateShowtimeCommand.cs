using Application.Features.Showtimes.Responses;
using Application.Common.Models;
using Domain.Enums;
namespace Application.Features.Showtimes.Commands.Create;

public sealed record CreateShowtimeCommand(
    int MovieId,
    int HallId,
    DateOnly Date,
    DateTime StartTime,
    DateTime EndTime,
    decimal BasePrice,
    ShowtimeStatus Status) : IRequest<ApiResponse<CreateShowtimeResponse>>;
