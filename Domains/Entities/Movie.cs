using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DurationMinutes { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string PosterUrl { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public AgeRating AgeRating { get; set; }
    public decimal AverageRating { get; set; }
    public MovieStatus Status { get; set; }
    public ICollection<MovieActor> MovieActors { get; set; } = [];
    public ICollection<MovieGenre> MovieGenres { get; set; } = [];
    public ICollection<Showtime> Showtimes { get; set; } = [];
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Favorite> Favorites { get; set; } = [];
}