using System.Collections.Generic;

namespace Application.Features.Actors.Responses;

public sealed record GetActorsResponse(IReadOnlyList<ActorResponse> Items);
