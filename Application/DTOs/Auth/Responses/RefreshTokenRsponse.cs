using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Auth.Responses
{
    public sealed record RefreshTokenResponse(
        string AccessToken,
        string RefreshToken
    );
}
