using Domain.Entities.Identity;

namespace Domain.Entities;

public class Favorite
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public int MovieId { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}