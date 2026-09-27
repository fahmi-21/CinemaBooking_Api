using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Queries.GetBranches
{
    public sealed record GetBranchesQuery(
        int PageNumber = 1,
        int PageSize = 10
        ) : IRequest<GetBranchesResponse>;

}
