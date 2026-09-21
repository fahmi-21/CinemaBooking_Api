using Domain.Entities.Common;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class RefreshToken : BaseEntity
{
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }

    public ApplicationUser User { get; set; } = null!;
}