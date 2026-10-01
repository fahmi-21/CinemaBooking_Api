using System;
using System.Collections.Generic;
using System.Text;


namespace Application.DTOs.Auth.Responses
{
    public record RegisterResponse(
    Guid UserId,
    string? Email
    );
}
