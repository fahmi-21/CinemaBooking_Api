using Application.Common.Models;
using Application.Features.Reviews.Responses;

namespace Application.Features.Reviews.Queries.GetReviewById;

public sealed record GetReviewByIdQuery(int Id) : IRequest<ApiResponse<ReviewResponse>>;
