using Application.Abstractions.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Authentication.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IAppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public RefreshTokenCommandHandler(
        IAppDbContext context,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _context = context;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<RefreshTokenResponse> Handle( RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // 1. Find refresh token
        var refreshToken = await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync( x => x.Token == request.RefreshToken,cancellationToken);

        // 2. Check token exists
        if (refreshToken is null)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        // 3. Check token revoked
        if (refreshToken.IsRevoked)
            throw new UnauthorizedAccessException("Refresh token has been revoked.");

        // 4. Check token expiration
        if (refreshToken.ExpiresAt <= DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token has expired.");

        // 5. Generate new access token
        var newAccessToken =
            await _tokenService.GenerateAccessTokenAsync(
                refreshToken.User);

        // 6. Generate new refresh token
        var newRefreshTokenValue =
            _tokenService.GenerateRefreshToken();

        // 7. Revoke old refresh token
        refreshToken.IsRevoked = true;
        refreshToken.SetUpdatedAt();

        // 8. Create new refresh token entity
        var newRefreshToken = new Domain.Entities.RefreshToken
        {
            UserId = refreshToken.UserId,
            Token = newRefreshTokenValue,
            ExpiresAt = DateTime.UtcNow.AddDays(
                int.Parse(
                    _configuration[
                        "Jwt:RefreshTokenExpirationDays"
                    ]!
                )
            )
        };

        // 9. Set creation date
        newRefreshToken.SetCreatedAt();

        // 10. Add new refresh token
        _context.RefreshTokens.Add(newRefreshToken);

        // 11. Save changes
        await _context.SaveChangesAsync(cancellationToken);

        // 12. Return new tokens
        return new RefreshTokenResponse(
            newAccessToken,
            newRefreshTokenValue);
    }
}