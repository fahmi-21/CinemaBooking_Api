using Domain.Enums;
namespace Api.Contracts.Showtimes;

public sealed record UpdateShowtimeRequest(
    int MovieId,
    int HallId,
    DateOnly Date,
    DateTime StartTime,
    DateTime EndTime,
    decimal BasePrice,
    ShowtimeStatus Status);
