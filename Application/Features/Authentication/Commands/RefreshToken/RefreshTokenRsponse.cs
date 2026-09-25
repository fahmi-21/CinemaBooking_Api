using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.RefreshToken
{
    public sealed record RefreshTokenResponse(
        string AccessToken,
        string RefreshToken
    );
}
