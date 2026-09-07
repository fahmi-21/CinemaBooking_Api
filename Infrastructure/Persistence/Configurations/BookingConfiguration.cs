using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TotalAmount)
                .HasPrecision(10, 2);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Showtime)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.ShowtimeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.BookingSeats)
                .WithOne(x => x.Booking)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Payments)
                .WithOne(x => x.Booking)
                .HasForeignKey(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Ticket)
                .WithOne(x => x.Booking)
                .HasForeignKey<Ticket>(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.CouponRedemption)
                .WithOne(x => x.Booking)
                .HasForeignKey<CouponRedemption>(x => x.BookingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
    
    }
}
