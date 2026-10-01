using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Reviews.Responses;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Commands.Create;

public sealed class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ApiResponse<CreateReviewResponse>>
{
    private readonly IAppDbContext _context;

    public CreateReviewCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<CreateReviewResponse>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = new Review
        {
                UserId = request.UserId,
                MovieId = request.MovieId,
                Rating = request.Rating,
                Comment = request.Comment,
                IsApproved = request.IsApproved
        };
            entity.SetCreatedAt();
        await _context.Reviews.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse<CreateReviewResponse>(
            true,
            "Review created successfully.",
            new CreateReviewResponse(entity.Id));
    }
}
