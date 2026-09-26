using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Persistence
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
