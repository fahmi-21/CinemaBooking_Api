using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Models;
using MediatR;

namespace Application.Features.Authentication.Commands.Logout
{
    public sealed record LogoutCommand (
        string RefreshToken
    ) : IRequest<ApiResponse<EmptyResponse>>;   
}
