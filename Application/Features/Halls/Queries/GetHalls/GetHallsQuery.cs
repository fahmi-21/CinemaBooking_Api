using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHalls
{
    public sealed record GetHallsQuery (
        int PageNumber = 1,
        int PageSize = 10
        ) : IRequest<ApiResponse<GetHallsResponse>>;
}
