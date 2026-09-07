using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class CouponRedemptionConfiguration : IEntityTypeConfiguration<Domain.Entities.CouponRedemption>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Entities.CouponRedemption> builder)
        {
            builder.HasKey(  e => e.Id);

            builder.HasOne(e => e.Coupon)
                .WithMany(e => e.CouponRedemptions)
                .HasForeignKey(e => e.CouponId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne( e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(e => e.Booking)
                .WithOne(e => e.CouponRedemption)
                .HasForeignKey<CouponRedemption>(e => e.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => new
            {
                e.CouponId,
                e.UserId
            })
            .IsUnique();

        }
    }
}
