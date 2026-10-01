using Application.Abstractions;
using Application.Common.Models;
using Application.Features.Authentication.Commands.Logout;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Authentication.Commands.Logout
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, ApiResponse<EmptyResponse>>
    {
        private readonly ITokenService _tokenService;
        private readonly IAppDbContext _context;
        public LogoutCommandHandler ( ITokenService tokenService , IAppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<ApiResponse<EmptyResponse>> Handle ( LogoutCommand request , CancellationToken cancellationToken)
        {
            // get refresh token from database
            var refreshToken = await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == request.RefreshToken,
                cancellationToken);


            if (refreshToken is null)
                return new ApiResponse<EmptyResponse>(true, "Logged out successfully.", new EmptyResponse());

            //
            refreshToken.IsRevoked = true;
            refreshToken.SetUpdatedAt();

            await _context.SaveChangesAsync(cancellationToken);
            return new ApiResponse<EmptyResponse>(true, "Logged out successfully.", new EmptyResponse());
        }

    }
}
