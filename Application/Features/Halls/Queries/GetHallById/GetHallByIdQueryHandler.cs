using Application.Abstractions;
using Application.Common.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Features.Halls.Queries.GetHallById
{
    public sealed class GetHallByIdQueryHandler : IRequestHandler<GetHallByIdQuery, ApiResponse<GetHallByIdResponse>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetHallByIdQueryHandler(IAppDbContext context , ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<GetHallByIdResponse>> Handle(GetHallByIdQuery request, CancellationToken cancellationToken)
        {
            var hall = await _context.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (hall is null)
                return new ApiResponse<GetHallByIdResponse>(false, "Hall not found.", null);

            var isGlobalUser = _currentUserService.IsInRole( Roles.ADMIN_ROLE)||
                                    _currentUserService.IsInRole(Roles.SUPER_ADMIN_ROLE);

            var isBranchMangar = _currentUserService.IsInRole(Roles.BRANCH_MANAGER_ROLE);

            var isCustomer = _currentUserService.IsInRole(Roles.CUSTOMER_ROLE);

            if (isBranchMangar)
            {
                var branchID = _currentUserService.BranchId;

                if (branchID is null )
                {
                    throw new UnauthorizedAccessException(" User is not Assigned to a branch");
                }

                if ( branchID.Value != hall.BranchId ) 
                {
                    throw new UnauthorizedAccessException(" Sorry you can't see another Branches Data ");
                }
            }
            else if (!isGlobalUser && !isCustomer)
            {
                throw new UnauthorizedAccessException(
                    "User is not authorized to view branches.");
            }


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
