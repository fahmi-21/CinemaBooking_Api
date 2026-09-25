using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ResetPassword
{
    public sealed record ResetPasswordCommand(
    Guid UserId,
    string Token,
    string NewPassword
    ) : IRequest<ResetPasswordResponse>;
}
