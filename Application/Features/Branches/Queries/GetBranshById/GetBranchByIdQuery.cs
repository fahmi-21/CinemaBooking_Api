using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Queries.GetBranshById
{
    public sealed record GetBranchByIdQuery(
        int Id) : IRequest<GetBranchByIdResponse?>;
}
