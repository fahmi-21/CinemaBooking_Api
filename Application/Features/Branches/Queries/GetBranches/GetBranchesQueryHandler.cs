using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Queries.GetBranches
{
    public sealed class GetBranchesQueryHandler : IRequestHandler<GetBranchesQuery, ApiResponse<GetBranchesResponse>>
    {
        private readonly IAppDbContext _context;
        public GetBranchesQueryHandler ( IAppDbContext context )
        {
            _context = context;
        }

        public async Task<ApiResponse<GetBranchesResponse>> Handle ( GetBranchesQuery request , CancellationToken cancellationToken )
        {
            var query = _context.Branches.AsNoTracking();

            var totalCount = await query.CountAsync( cancellationToken );

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var branches = await query
                .OrderBy(e => e.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BranchItemResponse(
                                   x.Id,
                                   x.Name,
                                   x.Address,
                                   x.Latitude,
                                   x.Longitude,
                                   x.GoogleMapsUrl
                               ))
                .ToListAsync(cancellationToken);

            return new ApiResponse<GetBranchesResponse>(
                true,
                "Branches retrieved successfully.",
                new GetBranchesResponse(
                    branches,
                    totalCount,
                    request.PageNumber,
                    request.PageSize,
                    totalPages));
        }
    }
}
