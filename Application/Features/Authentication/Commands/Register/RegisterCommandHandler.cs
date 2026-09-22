using Domain.Entities.Identity;
using Application.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Authentication.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public RegisterCommandHandler(UserManager<ApplicationUser> userManager , IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FName = request.FirstName,
            LName = request.LastName
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new RegisterResponse(Guid.Empty, null, $"Registration failed: {errors}");
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = System.Web.HttpUtility.UrlEncode(token);

        var confirmationLink =
            $"{_configuration["AppUrl"]}/api/auth/confirm-email" +
            $"?userId={user.Id}&token={encodedToken}";

        await _userManager.AddToRoleAsync(user, Roles.CUSTOMER_ROLE);

        return new RegisterResponse(user.Id, user.Email, "Registration successful.");
    }
}
    