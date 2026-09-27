using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Queries.GetBranshById
{
    public sealed record GetBranchByIdResponse(
        int Id,
        string Name,
        string Address,
        double? Latitude,
        double? Longitude,
        string GoogleMapsUrl
        );
}
