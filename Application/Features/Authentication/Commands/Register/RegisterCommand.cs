using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.Register
{
    public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword
    ) : IRequest<RegisterResponse>;
}
