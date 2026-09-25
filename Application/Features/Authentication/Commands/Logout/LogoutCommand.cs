using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Features.Authentication.Commands.Logout
{
    public sealed record LogoutCommand (
        string RefreshToken
    ) : IRequest;   
}
