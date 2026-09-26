using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Queries.ConfirmEmail
{
    public sealed record ConfirmEmailResponse
    (
        bool Succeeded,
        string Message
    );
}
