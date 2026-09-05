using Domain.Entities.Common;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }
    public byte Rating { get; set; }
    public string? Comment { get; set; }
    public bool IsApproved { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}