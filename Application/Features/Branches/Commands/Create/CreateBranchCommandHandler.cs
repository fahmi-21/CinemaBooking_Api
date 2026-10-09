using Application.Abstractions;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Create
{
    public sealed class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, ApiResponse<CreateBranchResponse>>
    {
        private readonly IAppDbContext _context;
        public CreateBranchCommandHandler ( IAppDbContext context )
        {
            _context = context;
        }

        public async Task<ApiResponse<CreateBranchResponse>> Handle ( CreateBranchCommand request ,CancellationToken cancellationToken )
        {
            var branchExists = await _context.Branches.AnyAsync(e => e.Name == request.Name, cancellationToken);

            if (branchExists)
                return new ApiResponse<CreateBranchResponse>(false, "Branch is already Found", null);

            var branch = new Branch
            {
                Name = request.Name,
                Address = request.Address,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                GoogleMapsUrl = request.GoogleMapsUrl
            };

            branch.SetCreatedAt();
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApiResponse<CreateBranchResponse>(
                             true,
                             "Branch created successfully.",
                             new CreateBranchResponse(branch.Id));
        }
    }
}
