using Domain.Entities.Identity;

namespace Domain.Entities;

public class Favorite
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid MovieId { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Movie Movie { get; set; } = null!;
}