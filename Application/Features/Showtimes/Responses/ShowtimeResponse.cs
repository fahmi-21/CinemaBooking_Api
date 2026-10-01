using Domain.Enums;
namespace Application.Features.Showtimes.Responses;

public sealed record ShowtimeResponse(
    int Id,
    int MovieId,
    int HallId,
    DateOnly Date,
    DateTime StartTime,
    DateTime EndTime,
    decimal BasePrice,
    ShowtimeStatus Status);
