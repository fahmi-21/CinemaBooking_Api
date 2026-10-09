using Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(  IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var userId = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier);

                if (!Guid.TryParse(userId, out var id))
                {
                    throw new UnauthorizedAccessException(
                        "User is not authenticated.");
                }

                return id;
            }
        }
        public int? BranchId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?
                    .User
                    .FindFirst("branch_id")?
                    .Value;

                return int.TryParse(value, out var branchId)
                    ? branchId
                    : null;
            }
        }
        public bool IsInRole(string role) =>
            _httpContextAccessor.HttpContext?.User.IsInRole(role) == true;
    }
}
