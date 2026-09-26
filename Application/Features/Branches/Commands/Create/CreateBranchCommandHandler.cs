using Application.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Create
{
    public sealed class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand , int>
    {
        private readonly IAppDbContext _context;
        public CreateBranchCommandHandler ( IAppDbContext context )
        {
            _context = context;
        }

        public async Task<int> Handle ( CreateBranchCommand request ,CancellationToken cancellationToken )
        {
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

            return branch.Id;
        }
    }
}
