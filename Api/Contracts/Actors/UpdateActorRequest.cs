namespace Api.Contracts.Actors;

public sealed record UpdateActorRequest(
    string FullName,
    string? PhotoUrl);
