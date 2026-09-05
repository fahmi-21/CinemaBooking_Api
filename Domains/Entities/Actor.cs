using Domain.Entities.Common;

namespace Domain.Entities;

public class Actor : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public ICollection<MovieActor> MovieActors { get; set; } = [];
}