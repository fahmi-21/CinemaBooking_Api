using Application.Abstractions;
using Application.Common.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHalls
{
    public class GetHallsQueryHandler : IRequestHandler< GetHallsQuery , ApiResponse<GetHallsResponse>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetHallsQueryHandler( IAppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }


        public async Task<ApiResponse<GetHallsResponse>> Handle(GetHallsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Halls.AsNoTracking();

            var isGlobalUser = _currentUserService.IsInRole(Roles.ADMIN_ROLE) ||
                                    _currentUserService.IsInRole(Roles.SUPER_ADMIN_ROLE);

            var isBranchUser =_currentUserService.IsInRole(Roles.BRANCH_MANAGER_ROLE) ||
                                    _currentUserService.IsInRole(Roles.EMPLOYEE_ROLE);

            var isCustomer = _currentUserService.IsInRole(Roles.CUSTOMER_ROLE);

            if (isBranchUser)
            {
                var branchId = _currentUserService.BranchId;

                if (branchId is null)
                {
                    throw new UnauthorizedAccessException(
                        "User is not assigned to a branch.");
                }

                query = query.Where(hall => hall.BranchId == branchId.Value);
            }
            else if (!isGlobalUser && !isCustomer)
            {
                var branchId = _currentUserService.BranchId;

                if (branchId is null)
                {
                    throw new UnauthorizedAccessException(
                        "User is not assigned to a branch.");
                }

                query = query.Where(hall => hall.BranchId == branchId.Value);
            }

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
