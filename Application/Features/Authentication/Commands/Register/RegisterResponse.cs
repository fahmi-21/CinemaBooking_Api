using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Features.Authentication.Commands.Register
{
    public record RegisterResponse(
    Guid UserId,
    string? Email,
    string Message
    );
}
