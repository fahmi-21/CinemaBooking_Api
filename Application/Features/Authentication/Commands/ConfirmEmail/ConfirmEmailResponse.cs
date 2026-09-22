using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ConfirmEmail
{
    public sealed record ConfirmEmailResponse
    (
        bool Succeeded,
        string Message
    );
}
