using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Create
{
    public sealed class CreateHallCommandHandler : IRequestHandler<CreateHallCommand, ApiResponse<CreateHallResponse>>
    {
        private readonly IAppDbContext _context; 
        public CreateHallCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<CreateHallResponse>> Handle ( CreateHallCommand request , CancellationToken cancellationToken)
        {
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
