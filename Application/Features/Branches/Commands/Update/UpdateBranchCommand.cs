using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Update
{
    public sealed record UpdateBranchCommand(
        int Id,
        string Name,
        string Address,
        double? Latitude,
        double? Longitude,
        string GoogleMapsUrl
        ) : IRequest<ApiResponse<UpdateBranchResponse>>;
    
}
