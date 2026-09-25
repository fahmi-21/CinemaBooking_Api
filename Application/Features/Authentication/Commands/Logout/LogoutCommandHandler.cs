using Application.Abstractions.Persistence;
using Application.Features.Authentication.Commands.Logout;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authentication.Commands.Logout
{
    public sealed class LogoutCommandHandler
    {
        private readonly ITokenService _tokenService;
        private readonly IAppDbContext _context;
        public LogoutCommandHandler ( ITokenService tokenService , IAppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task Handle ( LogoutCommand request , CancellationToken cancellationToken)
        {
            // get refresh token from database
            var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken,
                cancellationToken);


            if (refreshToken is null)
                return;

            //
            refreshToken.IsRevoked = true;
            refreshToken.SetUpdatedAt();

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
