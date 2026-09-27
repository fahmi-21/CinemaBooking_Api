using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Queries.GetBranches
{
    public sealed record GetBranchesResponse(
    IReadOnlyList<BranchItemResponse> Items,
    int TotalCount,
    int PageNumber,
    int PageSize,
    int TotalPages
    );

    public sealed record BranchItemResponse(
        int Id,
        string Name,
        string Address,
        double? Latitude,
        double? Longitude,
        string GoogleMapsUrl);
}
