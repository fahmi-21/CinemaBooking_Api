using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
namespace Application.Features.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler< ResetPasswordCommand , ResetPasswordResponse >
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ResetPasswordResponse> Handle ( ResetPasswordCommand request , CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user is null)
            {
                return new ResetPasswordResponse( false , "User not found.");
            }

            string decodedToken;

            try
            {
                decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            }
            catch
            {
                return new ResetPasswordResponse(false,"Invalid reset token.");
            }

            var result = await _userManager.ResetPasswordAsync(user,  decodedToken , request.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ",result.Errors.Select(e => e.Description));

                return new ResetPasswordResponse(false , $"Password reset failed: {errors}");
            }

            return new ResetPasswordResponse( true , "Password has been reset successfully.");

        }
    }
}
