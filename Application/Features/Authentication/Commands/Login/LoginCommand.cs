using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Models;
using Application.DTOs.Auth.Responses;
using MediatR;

namespace Application.Features.Authentication.Commands.Login
{
    public sealed record LoginCommand(
    string Email,
    string Password
           ) : IRequest<ApiResponse<LoginResponse>>;
}
