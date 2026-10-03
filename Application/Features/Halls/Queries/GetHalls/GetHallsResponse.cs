using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHalls
{
    public sealed record GetHallsResponse(
        IReadOnlyList<GetHallsResponseItem> Items,
        int TotalCount,
        int TotalPages,
        int PageNumber,
        int PageSize
    );

    public sealed record GetHallsResponseItem(
        int Id,
        int BranchId,
        string Name,
        HallType Type,
        int Capacity,
        int CleaningBufferMinutes
    );
}
