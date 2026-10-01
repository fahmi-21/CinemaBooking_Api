using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Models;
using MediatR;

namespace Application.Features.Authentication.Commands.ForgotPassword
{
    public sealed record ForgorPasswordCommand(
        string Email
        ) : IRequest<ApiResponse<EmptyResponse>>;
}
