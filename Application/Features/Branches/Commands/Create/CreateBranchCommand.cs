using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Create
{
    public sealed record CreateBranchCommand(
    string Name,
    string Address,
    double? Latitude,
    double? Longitude,
    string GoogleMapsUrl
    ) : IRequest<ApiResponse<CreateBranchResponse>>;
}
