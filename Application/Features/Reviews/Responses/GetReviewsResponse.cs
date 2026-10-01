using System.Collections.Generic;

namespace Application.Features.Reviews.Responses;

public sealed record GetReviewsResponse(IReadOnlyList<ReviewResponse> Items);
