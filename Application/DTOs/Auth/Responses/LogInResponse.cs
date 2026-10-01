using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Auth.Responses
{
    public sealed record LoginResponse(
        Guid UserId,
        string Email,
        string AccessToken,
        string RefreshToken
    );
}
