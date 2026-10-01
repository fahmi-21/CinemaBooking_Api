using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models
{
    public sealed record ApiResponse<T>(
    bool Success,
    string Message,
    T? Data
    );
}
