using Application.Abstractions.Persistence;
using Domain.Entities;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Authentication.Commands.ForgotPassword
{
    public sealed class ForgotPasswordCommandHandler: IRequestHandler<ForgorPasswordCommand, ForgotPasswordResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<ForgotPasswordResponse> Handle ( ForgorPasswordCommand request , CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return new ForgotPasswordResponse(
                    true,
                    "If the email exists, a password reset link has been sent."
                );
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user);
            var encodedtoken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var appUrl = _configuration["AppUrl"] ?? "https://localhost:53231";

            var resetLink = $"{appUrl}/api/Auth/ResetPassword" +
                $"?userId={user.Id}&token={encodedtoken}";

            var body = " <h2>Password Reset</h2>\r\n\r\n" +
                $"<p>\r\n We received a request to reset your password.\r\n </p>\r\n\r\n" +
                $"<p>\r\n   <a href=\"{resetLink}\">\r\n Reset my password\r\n   </a>\r\n </p>\r\n\r\n" +
                $"<p>\r\n   If you did not request this, you can ignore this email.\r\n</p>";

            await _emailService.SendEmailAsync( user.Email , "Reset your password", body , cancellationToken);

            return new ForgotPasswordResponse(
            true,
            "If the email exists, a password reset link has been sent."
            );
        }
    }
}
