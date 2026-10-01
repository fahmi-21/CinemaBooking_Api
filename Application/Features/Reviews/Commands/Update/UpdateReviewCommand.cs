using Application.Features.Reviews.Responses;
using Application.Common.Models;
namespace Application.Features.Reviews.Commands.Update;

public sealed record UpdateReviewCommand(
    int Id,
    Guid UserId,
    int MovieId,
    byte Rating,
    string? Comment,
    bool IsApproved) : IRequest<ApiResponse<UpdateReviewResponse>>;
