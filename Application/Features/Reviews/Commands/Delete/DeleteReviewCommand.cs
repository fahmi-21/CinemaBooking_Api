using Application.Common.Models;

namespace Application.Features.Reviews.Commands.Delete;

public sealed record DeleteReviewCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
