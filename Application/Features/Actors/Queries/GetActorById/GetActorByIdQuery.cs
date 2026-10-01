using Application.Common.Models;
using Application.Features.Actors.Responses;

namespace Application.Features.Actors.Queries.GetActorById;

public sealed record GetActorByIdQuery(int Id) : IRequest<ApiResponse<ActorResponse>>;
