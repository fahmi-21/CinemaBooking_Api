using System.Collections.Generic;

namespace Application.Features.Showtimes.Responses;

public sealed record GetShowtimesResponse(IReadOnlyList<ShowtimeResponse> Items);
