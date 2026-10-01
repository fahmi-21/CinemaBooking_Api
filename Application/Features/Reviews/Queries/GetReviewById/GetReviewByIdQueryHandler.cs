using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Reviews.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Queries.GetReviewById;

public sealed class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ApiResponse<ReviewResponse>>
{
    private readonly IAppDbContext _context;

    public GetReviewByIdQueryHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<ReviewResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Reviews
            .AsNoTracking()
            .Where(e => e.Id == request.Id)
            .Select(e => new ReviewResponse(
                    e.Id,
                    e.UserId,
                    e.MovieId,
                    e.Rating,
                    e.Comment,
                    e.IsApproved))
            .FirstOrDefaultAsync(cancellationToken);

        return entity is null
            ? new ApiResponse<ReviewResponse>(false, "Review not found.", null)
            : new ApiResponse<ReviewResponse>(true, "Review retrieved successfully.", entity);
    }
}
