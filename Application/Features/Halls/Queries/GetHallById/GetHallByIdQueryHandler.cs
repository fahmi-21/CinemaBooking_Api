using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHallById
{
    public sealed class GetHallByIdQueryHandler : IRequestHandler<GetHallByIdQuery, ApiResponse<GetHallByIdResponse>>
    {
        private readonly IAppDbContext _context;

        public GetHallByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<GetHallByIdResponse>> Handle(GetHallByIdQuery request, CancellationToken cancellationToken)
        {
            var hall = await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync( e => e.Id == request.Id, cancellationToken) ;


            if (hall is null)
                return new ApiResponse<GetHallByIdResponse>(false, "Hall not found.", null);

            return new ApiResponse<GetHallByIdResponse>(
                true,
                "Hall retrieved successfully.",
                new GetHallByIdResponse(
                    hall.Id,
                    hall.BranchId,
                    hall.Name,
                    hall.Type,
                    hall.Capacity,
                    hall.CleaningBufferMinutes));
        }

    }
}
