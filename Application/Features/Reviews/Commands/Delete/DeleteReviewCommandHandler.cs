using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reviews.Commands.Delete;

public sealed class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ApiResponse<EmptyResponse?>>
{
    private readonly IAppDbContext _context;

    public DeleteReviewCommandHandler(IAppDbContext context) => _context = context;

    public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Reviews.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (entity is null)
            return new ApiResponse<EmptyResponse?>(false, "Review not found.", null);

        _context.Reviews.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return new ApiResponse<EmptyResponse?>(true, "Review deleted successfully.", null);
    }
}
