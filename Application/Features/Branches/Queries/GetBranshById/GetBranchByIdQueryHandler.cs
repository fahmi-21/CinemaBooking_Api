using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Queries.GetBranshById
{
    public sealed class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, ApiResponse<GetBranchByIdResponse>>
    {
        private readonly IAppDbContext _context;
        
        public GetBranchByIdQueryHandler ( IAppDbContext context )
        {
            _context = context;
        }

        public async Task<ApiResponse<GetBranchByIdResponse>> Handle ( GetBranchByIdQuery request , CancellationToken  cancellationToken)
        {
            var branch = await _context.Branches.AsNoTracking()
                .Where(e => e.Id == request.Id)
                .Select(x => new GetBranchByIdResponse(
                x.Id,
                x.Name,
                x.Address,
                x.Latitude,
                x.Longitude,
                x.GoogleMapsUrl
            )).FirstOrDefaultAsync(cancellationToken);

            return branch is null
                ? new ApiResponse<GetBranchByIdResponse>(false, "Branch not found.", null)
                : new ApiResponse<GetBranchByIdResponse>(true, "Branch retrieved successfully.", branch);
        }
    }
}
