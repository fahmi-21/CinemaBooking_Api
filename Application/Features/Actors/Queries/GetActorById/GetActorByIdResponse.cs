namespace Application.Features.Actors.Queries.GetActorById;

public sealed record GetActorByIdResponse(
    int Id,
    string FullName,
    string? PhotoUrl);
