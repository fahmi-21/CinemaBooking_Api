using Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Queries.ConfirmEmail
{
    public sealed record ConfirmEmailCommand
    (
       Guid UserId,
       string Token) : IRequest<ApiResponse<EmptyResponse>>;
    
}
