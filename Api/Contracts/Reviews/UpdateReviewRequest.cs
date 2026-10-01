namespace Api.Contracts.Reviews;

public sealed record UpdateReviewRequest(
    Guid UserId,
    int MovieId,
    byte Rating,
    string? Comment,
    bool IsApproved);
