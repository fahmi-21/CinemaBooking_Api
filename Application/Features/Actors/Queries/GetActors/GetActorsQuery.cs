using Application.Common.Models;
using Application.Features.Actors.Responses;

namespace Application.Features.Actors.Queries.GetActors;

public sealed record GetActorsQuery : IRequest<ApiResponse<GetActorsResponse>>;
