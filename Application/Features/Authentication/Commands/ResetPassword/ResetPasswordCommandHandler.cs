using Application.Common.Models;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
namespace Application.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ApiResponse<EmptyResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<EmptyResponse>> Handle ( ResetPasswordCommand request , CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
            {
                return new ApiResponse<EmptyResponse>(false, "User not found.", null);
            }

            string decodedToken;

            try
            {
                decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            }
            catch
            {
                return new ApiResponse<EmptyResponse>(false, "Invalid reset token.", null);
            }

            var result = await _userManager.ResetPasswordAsync(user,  decodedToken , request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ",result.Errors.Select(e => e.Description));

                return new ApiResponse<EmptyResponse>(false, $"Password reset failed: {errors}", null);
            }

            return new ApiResponse<EmptyResponse>(true, "Password has been reset successfully.", new EmptyResponse());

        }
    }
}
