using Application.Abstractions;
using Application.Common.Models;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
namespace Application.Features.Authentication.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResponse<EmptyResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<EmptyResponse>> Handle ( ChangePasswordCommand request , CancellationToken cancellationToken)
        {
            var userId =  _currentUserService.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return new ApiResponse<EmptyResponse>(false, "User not found.", null);
            }

            var result = await _userManager.ChangePasswordAsync(
            user,
            request.CurrentPassword,
            request.NewPassword);

            if(!result.Succeeded)
        {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                return new ApiResponse<EmptyResponse>(false, $"Password change failed: {errors}", null);
            }

            return new ApiResponse<EmptyResponse>(true, "Password changed successfully.", new EmptyResponse());
        }
    }
}
