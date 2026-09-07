using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Amount)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(e => e.IdempotencyKey)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(e => e.IdempotencyKey)
            .IsUnique();

        builder.Property(e => e.ProviderReference)
            .HasMaxLength(200);

        builder.HasOne(e => e.Booking)
            .WithMany(e => e.Payments)
            .HasForeignKey(e => e.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}