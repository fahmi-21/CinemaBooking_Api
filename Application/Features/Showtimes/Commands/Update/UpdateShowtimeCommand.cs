using Application.Features.Showtimes.Responses;
using Application.Common.Models;
using Domain.Enums;
namespace Application.Features.Showtimes.Commands.Update;

public sealed record UpdateShowtimeCommand(
    int Id,
    int MovieId,
    int HallId,
    DateOnly Date,
    DateTime StartTime,
    DateTime EndTime,
    decimal BasePrice,
    ShowtimeStatus Status) : IRequest<ApiResponse<UpdateShowtimeResponse>>;
