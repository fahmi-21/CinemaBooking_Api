using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ConfirmEmail
{
    public sealed record ConfirmEmailCommand
    (
       Guid UserId,
       string Token) : IRequest<ConfirmEmailResponse>;
    
}
