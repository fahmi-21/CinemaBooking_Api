using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Constants;

namespace Application.Features.Halls.Commands.Update
{
    public sealed class UpdateHallCommandHandler : IRequestHandler< UpdateHallCommand  , ApiResponse<UpdateHallResponse> >
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateHallCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }


        public async Task< ApiResponse<UpdateHallResponse>> Handle ( UpdateHallCommand request , CancellationToken cancellationToken )
        {
            var isGlobalUser = _currentUserService.IsInRole(Roles.ADMIN_ROLE) ||
                                    _currentUserService.IsInRole(Roles.SUPER_ADMIN_ROLE);

            var isBranchManager = _currentUserService.IsInRole(Roles.BRANCH_MANAGER_ROLE);

            if (!isGlobalUser && !isBranchManager)
            {
                throw new UnauthorizedAccessException(
                    "User is not authorized to update halls.");
            }

            var hallsQuery = _context.Halls.AsQueryable();
            int? branchId = null;

            if (isBranchManager)
            {
                branchId = _currentUserService.BranchId;
                if (branchId is null)
                {
                    throw new UnauthorizedAccessException(
                        "User is not assigned to a branch.");
                }
                hallsQuery = hallsQuery.Where(hall => hall.BranchId == branchId.Value);
            }

            var hall = await hallsQuery.FirstOrDefaultAsync(hall => hall.Id == request.Id, cancellationToken);

            if (hall is null)
                return new ApiResponse<UpdateHallResponse>(
                    false,
                    " hall not found"
                    , null);

            if (isBranchManager && request.BranchId != branchId!.Value)
            {
                throw new UnauthorizedAccessException(
                    "A branch manager cannot move a hall to another branch.");
            }

            var branchExists = await _context.Branches.AnyAsync( branch => branch.Id == request.BranchId,cancellationToken);

            if (!branchExists)
            {
                return new ApiResponse<UpdateHallResponse>(
                    false,
                    "Branch not found.",
                    null);
            }
            var hallName = request.Name.Trim();

            var hallNameExists = await _context.Halls.AnyAsync( existingHall =>
               existingHall.BranchId == request.BranchId &&
               existingHall.Name == hallName &&
               existingHall.Id != request.Id,
               cancellationToken);

            if (hallNameExists)
            {
                return new ApiResponse<UpdateHallResponse>(
                    false,
                    "A hall with this name already exists in this branch.",
                    null);
            }

            hall.Name = hallName;
            hall.BranchId = request.BranchId;
            hall.Type = request.Type;
            hall.Capacity = request.Capacity;
            hall.CleaningBufferMinutes = request.CleaningBufferMinutes;
            hall.SetUpdatedAt();

            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResponse<UpdateHallResponse>(
                true,
                "hall updated successfully",
                new UpdateHallResponse(hall.Id)); 
        }
    }
}
