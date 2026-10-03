using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHalls
{
    public class GetHallsQueryHandler : IRequestHandler< GetHallsQuery , ApiResponse<GetHallsResponse>>
    {
        private readonly IAppDbContext _context;
        public GetHallsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        
        public async Task<ApiResponse<GetHallsResponse>> Handle(GetHallsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Halls.AsNoTracking();

            var totalCount = await query.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var halls = await query.OrderBy(x => x.Id)
                                   .Skip((request.PageNumber - 1) * request.PageSize)
                                   .Take(request.PageSize)
                                   .Select(x => new GetHallsResponseItem(
                                        x.Id,
                                        x.BranchId,
                                        x.Name,
                                        x.Type,
                                        x.Capacity,
                                        x.CleaningBufferMinutes))
                                   .ToListAsync(cancellationToken);

            return new ApiResponse<GetHallsResponse>(
                true,
                "Halls retrieved successfully.", 
                new GetHallsResponse( halls, 
                                      totalCount, 
                                      totalPages , 
                                      request.PageNumber , 
                                      request.PageSize));
        }
    }
}
