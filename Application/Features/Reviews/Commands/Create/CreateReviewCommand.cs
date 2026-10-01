using Application.Features.Reviews.Responses;
using Application.Common.Models;
namespace Application.Features.Reviews.Commands.Create;

public sealed record CreateReviewCommand(
    Guid UserId,
    int MovieId,
    byte Rating,
    string? Comment,
    bool IsApproved) : IRequest<ApiResponse<CreateReviewResponse>>;
