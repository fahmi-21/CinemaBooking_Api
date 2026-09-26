using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Delete
{
    public sealed class DeleteCommandHandler : IRequestHandler< DeleteCommand , bool >
    {
        private readonly IAppDbContext _context;

        public DeleteCommandHandler ( IAppDbContext context)
        {
            _context = context;
        }
        public async Task <bool> Handle ( DeleteCommand request , CancellationToken cancellationToken)
        {
            var branch = await _context.Branches.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (branch is null)
                return false;

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
