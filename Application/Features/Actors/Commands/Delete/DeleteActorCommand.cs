using Application.Common.Models;

namespace Application.Features.Actors.Commands.Delete;

public sealed record DeleteActorCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
