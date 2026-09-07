using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class ShowtimeConfiguration
    : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.BasePrice)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(e => e.StartTime)
            .IsRequired();

        builder.Property(e => e.EndTime)
            .IsRequired();

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired();

        builder.HasOne(e => e.Movie)
            .WithMany(e => e.Showtimes)
            .HasForeignKey(e => e.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Hall)
            .WithMany(e => e.Showtimes)
            .HasForeignKey(e => e.HallId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.ShowtimeSeats)
            .WithOne(e => e.Showtime)
            .HasForeignKey(e => e.ShowtimeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Bookings)
            .WithOne(e => e.Showtime)
            .HasForeignKey(e => e.ShowtimeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new
        {
            e.HallId,
            e.StartTime
        });
    }
}