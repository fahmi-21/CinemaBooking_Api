using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Application.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Delete
{
    public sealed class DeleteHallCommandHandler : IRequestHandler<DeleteHallCommand, ApiResponse<EmptyResponse?>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public DeleteHallCommandHandler ( IAppDbContext context  , ICurrentUserService currentUserService )
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteHallCommand request, CancellationToken cancellationToken)
        {
            var isGlobalUser = _currentUserService.IsInRole(Roles.ADMIN_ROLE) ||
                                    _currentUserService.IsInRole(Roles.SUPER_ADMIN_ROLE);
            var isBranchMangar = _currentUserService.IsInRole(Roles.BRANCH_MANAGER_ROLE);


            if ( !isBranchMangar && !isGlobalUser )
            {
                throw new UnauthorizedAccessException(
                    "User is not authorized to delete halls.");
            }

            var hallsQuery = _context.Halls.AsQueryable();

            if ( isBranchMangar)
            {
                var branchId =  _currentUserService.BranchId;

                if (branchId is null)
                {
                    throw new UnauthorizedAccessException("User Is not assigned to a branch.");
                }
                hallsQuery = hallsQuery.Where(e => e.BranchId == branchId.Value);
            }


            var hall = await hallsQuery.FirstOrDefaultAsync(hall => hall.Id == request.Id, cancellationToken);

            if (hall is null)
            {
                return new ApiResponse<EmptyResponse?>(
                    false,
                    "hall not found",
                    null
                    );
            }

            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResponse<EmptyResponse?>(
                true,
                "Hall deleted successfully.",
                null);
        }
    }
}
