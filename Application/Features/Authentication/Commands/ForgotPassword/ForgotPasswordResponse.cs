using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ForgotPassword
{
    public sealed record ForgotPasswordResponse(
    bool Succeeded,
    string Message
    );
}
