using Domain.Entities.Common;

namespace Domain.Entities;

public class Genre : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<MovieGenre> MovieGenres { get; set; } = [];
}