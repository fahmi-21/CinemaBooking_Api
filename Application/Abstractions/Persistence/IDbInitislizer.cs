using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Abstractions.Persistence
{
    public interface IDbInitislizer
    {
        Task InitializeAsync();
    }
}
