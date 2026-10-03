namespace Api.Contracts.Reviews;

public sealed record UpdateReviewRequest(
    int MovieId,
    byte Rating,
    string? Comment,
    bool IsApproved);
