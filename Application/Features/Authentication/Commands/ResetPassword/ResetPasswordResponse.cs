using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ResetPassword
{
    public sealed record ResetPasswordResponse(
    bool Succeeded,
    string Message
    );
}
