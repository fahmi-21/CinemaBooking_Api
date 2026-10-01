using Application.Abstractions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Delete
{
    public sealed class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, ApiResponse<EmptyResponse?>>
    {
        private readonly IAppDbContext _context;

        public DeleteBranchCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<ApiResponse<EmptyResponse?>> Handle ( DeleteBranchCommand request , CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (branch is null)
                return new ApiResponse<EmptyResponse?>(false, "Branch not found.", null);

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResponse<EmptyResponse?>(
                true,
                "Branch deleted successfully.",
                null);
        }
    }
}
