using Domain.Entities.Identity;
using Application.Abstractions.Persistence;
using Application.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Features.Authentication.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly ILogger<RegisterCommandHandler> _logger;

    public RegisterCommandHandler(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IEmailService emailService,
        ILogger<RegisterCommandHandler> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
        _logger = logger;
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

        await _userManager.AddToRoleAsync(user, Roles.CUSTOMER_ROLE);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = System.Web.HttpUtility.UrlEncode(token);

        var appUrl = _configuration["AppUrl"] ?? "https://localhost:53231";
        var confirmationLink =
            $"{appUrl}/api/Auth/ConfirmEmail" +
            $"?userId={user.Id}&token={encodedToken}";

        _logger.LogInformation("Sending confirmation email to {Email}. Link: {ConfirmationLink}", user.Email, confirmationLink);

        await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"<h1>Click <a href='{confirmationLink}'>here</a> to confirm your account</h1>", cancellationToken);

        return new RegisterResponse(user.Id, user.Email, "Registration successful.");
    }
}