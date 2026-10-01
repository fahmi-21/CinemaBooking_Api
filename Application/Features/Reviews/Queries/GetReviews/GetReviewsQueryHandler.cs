using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Reviews.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Queries.GetReviews;

public sealed class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, ApiResponse<GetReviewsResponse>>
{
    private readonly IAppDbContext _context;

    public GetReviewsQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<GetReviewsResponse>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        var items = await _context.Reviews
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new ReviewResponse(
                    e.Id,
                    e.UserId,
                    e.MovieId,
                    e.Rating,
                    e.Comment,
                    e.IsApproved))
            .ToListAsync(cancellationToken);

        return new ApiResponse<GetReviewsResponse>(
            true,
            "Reviews retrieved successfully.",
            new GetReviewsResponse(items));
    }
}
