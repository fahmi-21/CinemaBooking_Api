using Application.Common.Models;

namespace Application.Features.Actors.Queries.GetActorById;

public sealed record GetActorByIdQuery(int Id) : IRequest<ApiResponse<GetActorByIdResponse>>;
