using Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Delete
{
    public sealed class DeleteHallCommandHandler : IRequestHandler<DeleteHallCommand, ApiResponse<EmptyResponse?>>
    {
        private readonly IAppDbContext _context;
        public DeleteHallCommandHandler ( IAppDbContext context )
        {
            _context = context;
        }

        public async Task<ApiResponse<EmptyResponse?>> Handle(DeleteHallCommand request, CancellationToken cancellationToken)
        {
            var hall = await _context.Halls.FirstOrDefaultAsync( e => e.Id == request.Id , cancellationToken);

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
