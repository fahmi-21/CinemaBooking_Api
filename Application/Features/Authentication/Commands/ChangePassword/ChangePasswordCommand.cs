using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Models;
using MediatR;
namespace Application.Features.Authentication.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword
        ) : IRequest<ApiResponse<EmptyResponse>>;
}
