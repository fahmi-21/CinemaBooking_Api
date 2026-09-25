using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.Login
{
    public sealed record LoginResponse(
        Guid UserId,
        string Email,
        string AccessToken,
        string RefreshToken
    );
}
