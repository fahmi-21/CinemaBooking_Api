using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Update
{
    public sealed record UpdateHallCommand(
    int Id,
    int BranchId,
    string Name,
    HallType Type,
    int Capacity,
    int CleaningBufferMinutes
) : IRequest<ApiResponse<UpdateHallResponse>>;
}
