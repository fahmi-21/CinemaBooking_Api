using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Branches.Commands.Delete
{
    public sealed record DeleteCommand(
        int Id
        ) : IRequest<bool>;
}
