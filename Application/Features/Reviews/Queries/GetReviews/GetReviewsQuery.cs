using Application.Common.Models;
using Application.Features.Reviews.Responses;

namespace Application.Features.Reviews.Queries.GetReviews;

public sealed record GetReviewsQuery : IRequest<ApiResponse<GetReviewsResponse>>;
