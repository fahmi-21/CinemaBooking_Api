using Application.Common.Models;

namespace Application.Features.Actors.Queries.GetActors;

public sealed record GetActorsQuery(
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<ApiResponse<GetActorsResponse>>;
