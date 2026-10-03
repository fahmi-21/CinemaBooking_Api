using System.Collections.Generic;

namespace Application.Features.Actors.Queries.GetActors;

public sealed record GetActorsResponse(
   IReadOnlyList<ActorItemResponse> Items,
   int TotalCount,
   int PageNumber,
   int PageSize,
   int TotalPages
   );

public sealed record ActorItemResponse(
    int Id,
    string FullName,
    string PhotoUrl
    );