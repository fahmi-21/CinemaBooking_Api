using Application.Abstractions;
using Application.Common.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Create
{
    public sealed class CreateHallCommandHandler : IRequestHandler<CreateHallCommand, ApiResponse<CreateHallResponse>>
    {
        private readonly IAppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateHallCommandHandler(
            IAppDbContext context,
            ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<CreateHallResponse>> Handle ( CreateHallCommand request , CancellationToken cancellationToken)
        {
            var isGlobalUser = _currentUserService.IsInRole(Roles.ADMIN_ROLE) ||
                                    _currentUserService.IsInRole(Roles.SUPER_ADMIN_ROLE);

            var isBranchManager = _currentUserService.IsInRole(Roles.BRANCH_MANAGER_ROLE);

            if (!isGlobalUser && !isBranchManager)
            {
                throw new UnauthorizedAccessException(
                    "User is not authorized to create halls.");
            }

            if( isBranchManager)
            {
                var branchId = _currentUserService.BranchId;

                if (branchId is null)
                {
                    throw new UnauthorizedAccessException(
                        "User is not assigned to a branch.");
                }

                if (request.BranchId != branchId.Value)
                {
                    throw new UnauthorizedAccessException(
                        "User cannot create a hall outside the assigned branch.");
                }
            }

            var branchExists = await _context.Branches.AnyAsync(e => e.Id == request.BranchId, cancellationToken);
            if (!branchExists)
                return new ApiResponse<CreateHallResponse>(false, "Branch is not found", null);

            var hallNameExists = await _context.Halls.AnyAsync( hall => hall.BranchId == request.BranchId &&
            hall.Name == request.Name,cancellationToken);

            if (hallNameExists)
            {
                return new ApiResponse<CreateHallResponse>(
                    false,
                    "A hall with this name already exists in this branch.",
                    null);
            }

            var hall = new Domain.Entities.Hall
            {
                BranchId = request.BranchId,
                Name = request.Name,
                Type = request.Type,
                Capacity = request.Capacity,
                CleaningBufferMinutes = request.CleaningBufferMinutes
            };

            hall.SetCreatedAt();

            await _context.Halls.AddAsync(
            hall,
            cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResponse<CreateHallResponse>(
                true,
                "Hall Created Successfully",
                new CreateHallResponse(hall.Id)
            );        
        }
    }
}
