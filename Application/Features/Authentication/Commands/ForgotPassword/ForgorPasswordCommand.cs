using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Features.Authentication.Commands.ForgotPassword
{
    public sealed record ForgorPasswordCommand(
        string Email
        ) : IRequest<ForgotPasswordResponse>;
}
