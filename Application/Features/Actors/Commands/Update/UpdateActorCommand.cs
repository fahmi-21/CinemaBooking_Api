using Application.Features.Actors.Responses;
using Application.Common.Models;
namespace Application.Features.Actors.Commands.Update;

public sealed record UpdateActorCommand(
    int Id,
    string FullName,
    string? PhotoUrl) : IRequest<ApiResponse<UpdateActorResponse>>;
