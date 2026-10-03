using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Queries.GetHallById
{
    public sealed record GetHallByIdQuery(int Id) : IRequest<ApiResponse<GetHallByIdResponse>>;

}
