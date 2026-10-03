using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Update
{
    public sealed class UpdateHallCommandHandler : IRequestHandler< UpdateHallCommand  , ApiResponse<UpdateHallResponse> >
    {
        private readonly IAppDbContext _context;

        public UpdateHallCommandHandler(IAppDbContext  context)
        {
            _context = context;
        }

        public async Task< ApiResponse<UpdateHallResponse>> Handle ( UpdateHallCommand request , CancellationToken cancellationToken )
        {
            var hall = await _context.Halls.FirstOrDefaultAsync( e => e.Id == request.Id , cancellationToken);

            if (hall is null)
                return new ApiResponse<UpdateHallResponse>(
                    false,
                    " hall not found"
                    , null);

            var branchExists = await _context.Branches.AnyAsync(e => e.Id == request.BranchId, cancellationToken);
            if (!branchExists)
                return new ApiResponse<UpdateHallResponse>(false, "Branch not found.", null);

            hall.Name = request.Name;
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
