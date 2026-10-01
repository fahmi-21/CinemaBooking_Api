using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Update
{
    public sealed class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, ApiResponse<UpdateBranchResponse>>
    {
        private readonly IAppDbContext _context;
        public UpdateBranchCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<UpdateBranchResponse>> Handle (UpdateBranchCommand request , CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (branch is null)
                return new ApiResponse<UpdateBranchResponse>(false, "Branch not found.", null);

            branch.Name = request.Name;
            branch.Address = request.Address;
            branch.Latitude = request.Latitude;
            branch.Longitude = request.Longitude;
            branch.GoogleMapsUrl = request.GoogleMapsUrl;

            branch.SetUpdatedAt();

            await _context.SaveChangesAsync(cancellationToken);
            return new ApiResponse<UpdateBranchResponse>(
                true,
                "Branch updated successfully.",
                new UpdateBranchResponse(branch.Id));
        }
    }
}
