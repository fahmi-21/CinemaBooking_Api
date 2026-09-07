using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public string FtName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Booking> Bookings { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Favorite> Favorites { get; set; } = [];
    public ICollection<Notification> Notifications { get; set; } = [];
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}