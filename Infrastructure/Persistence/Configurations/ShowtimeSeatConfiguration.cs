using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class ShowtimeSeatConfiguration
    : IEntityTypeConfiguration<ShowtimeSeat>
{
    public void Configure(EntityTypeBuilder<ShowtimeSeat> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Price)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(e => e.RowVersion)
            .IsRowVersion()
            .IsConcurrencyToken();

        builder.HasOne(e => e.Showtime)
            .WithMany(e => e.ShowtimeSeats)
            .HasForeignKey(e => e.ShowtimeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Seat)
            .WithMany(e => e.ShowtimeSeats)
            .HasForeignKey(e => e.SeatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.LockedByUser)
            .WithMany()
            .HasForeignKey(e => e.LockedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(e => e.BookingSeats)
            .WithOne(e => e.ShowtimeSeat)
            .HasForeignKey(e => e.ShowtimeSeatId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new
        {
            e.ShowtimeId,
            e.SeatId
        })
        .IsUnique();
    }
}