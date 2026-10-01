using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Halls.Commands.Delete
{
    public sealed record DeleteHallCommand(int Id) : IRequest<ApiResponse<EmptyResponse?>>;
}
