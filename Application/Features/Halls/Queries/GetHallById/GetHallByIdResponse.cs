using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHallById
{
    public sealed record GetHallByIdResponse(
    int Id,
    int BranchId,
    string Name,
    HallType Type,
    int Capacity,
    int CleaningBufferMinutes);
}
