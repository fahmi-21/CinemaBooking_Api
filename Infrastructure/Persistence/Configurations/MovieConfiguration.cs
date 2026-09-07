using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure ( EntityTypeBuilder<Movie> builder )
        {
            builder.HasKey( e => e.Id );
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Description)
            .HasMaxLength(2000);

            builder.Property(e => e.DurationMinutes)
                .IsRequired();

            builder.Property ( e => e.PosterUrl)
                .IsRequired ()
                .HasMaxLength(500);

            builder.Property( e => e.TrailerUrl)
                .IsRequired ()
                .HasMaxLength(500);

            builder.Property( e => e.Language)
                .IsRequired ()
                .HasMaxLength(50);

            builder.Property( e=>e.Country)
                .IsRequired ()
                .HasMaxLength (100);

            builder.Property(e=>e.Director)
                .IsRequired ()
                .HasMaxLength(150);

            builder.Property(e=>e.AgeRating)
                .IsRequired ();

            builder.Property(e => e.AverageRating)
            .HasPrecision(3, 2);

            builder.Property( e=> e.Status)
                .IsRequired ();

            builder.HasIndex(e => e.Title);

            builder.HasMany(e => e.Showtimes)
            .WithOne(e => e.Movie)
            .HasForeignKey(e => e.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Reviews)
                .WithOne(e => e.Movie)
                .HasForeignKey(e => e.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(e => e.Favorites)
                .WithOne(e => e.Movie)
                .HasForeignKey(e => e.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
