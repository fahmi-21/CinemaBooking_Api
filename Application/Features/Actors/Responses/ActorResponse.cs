namespace Application.Features.Actors.Responses;

public sealed record ActorResponse(
    int Id,
    string FullName,
    string? PhotoUrl);
