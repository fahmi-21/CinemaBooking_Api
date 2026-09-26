using Application.Abstractions.Persistence;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
namespace Application.Features.Authentication.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler : IRequestHandler< ChangePasswordCommand , ChangePasswordResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<ChangePasswordResponse> Handle ( ChangePasswordCommand request , CancellationToken cancellationToken)
        {
            var userId =  _currentUserService.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
            {
                return new ChangePasswordResponse(  false, "User not found.");
            }

            var result = await _userManager.ChangePasswordAsync(
            user,
            request.CurrentPassword,
            request.NewPassword);

            if(!result.Succeeded)
        {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                return new ChangePasswordResponse(   false, $"Password change failed: {errors}");
            }

            return new ChangePasswordResponse( true, "Password changed successfully.");
        }
    }
}
