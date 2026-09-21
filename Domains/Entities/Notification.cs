using Domain.Entities.Common;
using Domain.Entities.Identity;
using Domain.Enums;

namespace Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public NotificationType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Body { get; set; }
    public bool IsRead { get; set; }

    public ApplicationUser User { get; set; } = null!;
}