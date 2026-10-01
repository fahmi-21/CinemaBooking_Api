namespace Application.Features.Reviews.Responses;

public sealed record ReviewResponse(
    int Id,
    Guid UserId,
    int MovieId,
    byte Rating,
    string? Comment,
    bool IsApproved);
