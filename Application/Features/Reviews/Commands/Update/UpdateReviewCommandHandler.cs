using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Reviews.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Commands.Update;

public sealed class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ApiResponse<UpdateReviewResponse>>
{
    private readonly IAppDbContext _context;

    public UpdateReviewCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<UpdateReviewResponse>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Reviews.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<UpdateReviewResponse>(false, "Review not found.", null);

        entity.UserId = request.UserId;
        entity.MovieId = request.MovieId;
        entity.Rating = request.Rating;
        entity.Comment = request.Comment;
        entity.IsApproved = request.IsApproved;
        entity.SetUpdatedAt();
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<UpdateReviewResponse>(
            true,
            "Review updated successfully.",
            new UpdateReviewResponse(entity.Id));
    }
}
