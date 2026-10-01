using Application.Features.Actors.Responses;
using Application.Common.Models;
namespace Application.Features.Actors.Commands.Create;

public sealed record CreateActorCommand(
    string FullName,
    string? PhotoUrl) : IRequest<ApiResponse<CreateActorResponse>>;
