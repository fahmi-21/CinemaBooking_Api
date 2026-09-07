using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class TicketConfiguration
    : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.TicketNumber)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e => e.TicketNumber)
            .IsUnique();

        builder.Property(e => e.QrCodePayload)
            .HasMaxLength(500);

        builder.Property(e => e.IssuedAt)
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired();

        builder.HasOne(e => e.Booking)
            .WithOne(e => e.Ticket)
            .HasForeignKey<Ticket>(e => e.BookingId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.BookingId)
            .IsUnique();
    }
}