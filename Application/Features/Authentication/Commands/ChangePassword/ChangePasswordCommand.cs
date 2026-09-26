using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
namespace Application.Features.Authentication.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(
        string CurrentPassword,
        string NewPassword
        ) : IRequest<ChangePasswordResponse>;
}
