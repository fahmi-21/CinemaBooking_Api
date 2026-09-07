using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class BookingSeatsConfiguration : IEntityTypeConfiguration<BookingSeat> 
    {
        public void Configure( EntityTypeBuilder<BookingSeat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.PriceAtBooking)
                   .HasPrecision(10, 2);

            builder.HasOne(x => x.Booking);
            builder.HasOne(x => x.ShowtimeSeat);

            builder.HasOne(x => x.Booking)
           .WithMany(x => x.BookingSeats)
           .HasForeignKey(x => x.BookingId)
           .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ShowtimeSeat)
            .WithMany(x => x.BookingSeats)
            .HasForeignKey(x => x.ShowtimeSeatId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
