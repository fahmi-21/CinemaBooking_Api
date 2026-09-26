using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Update
{
    public sealed class UpdateCommandHandler : IRequestHandler<UpdateCommand , bool>
    {
        private readonly IAppDbContext _context;
        public UpdateCommandHandler( IAppDbContext context)
        {
            _context = context;
        }

        public async Task <bool> Handle (UpdateCommand request , CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (branch is null)
                return false

            branch.Name = request.Name;
            branch.Address = request.Address;
            branch.Latitude = request.Latitude;
            branch.Longitude = request.Longitude;
            branch.GoogleMapsUrl = request.GoogleMapsUrl;

            branch.SetUpdatedAt();

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
