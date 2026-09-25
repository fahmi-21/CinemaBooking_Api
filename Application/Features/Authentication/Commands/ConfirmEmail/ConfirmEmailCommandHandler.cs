using Application.Features.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Constants;
using Domain.Entities.Identity;

namespace Application.Features.Authentication.Commands.ConfirmEmail
{
    public  sealed class ConfirmEmailCommandHandler :IRequestHandler<ConfirmEmailCommand, ConfirmEmailResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfirmEmailCommandHandler(
                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ConfirmEmailResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return new ConfirmEmailResponse(false, "User not found.");

            if (user.EmailConfirmed)
                return new ConfirmEmailResponse(false, "Email is already confirmed.");

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                return new ConfirmEmailResponse(false, $"Email confirmation failed: {errors}");
            }

            return new ConfirmEmailResponse(true, "Email confirmed successfully.");
        }
    }
       
}
