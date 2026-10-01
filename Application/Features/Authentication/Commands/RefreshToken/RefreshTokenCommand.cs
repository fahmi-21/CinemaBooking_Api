using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Models;
using Application.DTOs.Auth.Responses;
using MediatR;
namespace Application.Features.Authentication.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand(
        string RefreshToken
    ) : IRequest<ApiResponse<RefreshTokenResponse>>;
}
