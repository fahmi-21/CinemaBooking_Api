using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ChangePassword
{
    public sealed record ChangePasswordResponse
    (
        bool Succeeded,
        string Message
    );
}
