using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace Application.Features.Authentication.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand(
        string RefreshToken
    ) : IRequest<RefreshTokenResponse>;
}
