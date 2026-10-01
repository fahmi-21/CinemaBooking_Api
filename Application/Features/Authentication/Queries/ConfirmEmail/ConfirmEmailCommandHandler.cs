using Application.Features.Authentication.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Constants;
using Domain.Entities.Identity;
using Application.Common.Models;

namespace Application.Features.Authentication.Queries.ConfirmEmail
{
    public sealed class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ApiResponse<EmptyResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ConfirmEmailCommandHandler(
                UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ApiResponse<EmptyResponse>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());

            if (user == null)
                return new ApiResponse<EmptyResponse>(false, "User not found.", null);

            if (user.EmailConfirmed)
                return new ApiResponse<EmptyResponse>(false, "Email is already confirmed.", null);

            var result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                return new ApiResponse<EmptyResponse>(false, $"Email confirmation failed: {errors}", null);
            }

            return new ApiResponse<EmptyResponse>(true, "Email confirmed successfully.", new EmptyResponse());
        }
    }
       
}
