using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class SeatConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.RowLabel)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(e => e.SeatNumber)
            .IsRequired();

        builder.Property(e => e.SeatType)
            .IsRequired();

        builder.HasOne(e => e.Section)
            .WithMany(e => e.Seats)
            .HasForeignKey(e => e.SectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new
        {
            e.SectionId,
            e.RowLabel,
            e.SeatNumber
        })
        .IsUnique();
    }
}