using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Create
{
    public sealed record CreateHallCommand(
     int BranchId,
     string Name,
     HallType Type,
     int Capacity,
     int CleaningBufferMinutes
 ) : IRequest<ApiResponse<CreateHallResponse>>;
}
