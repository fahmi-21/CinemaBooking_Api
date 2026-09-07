using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Infrastructure.Persistence.Configurations
{
    internal class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure( EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Value)
               .IsRequired()
               .HasPrecision(10, 2);

            builder.HasIndex(e => e.Code)
                .IsUnique();

            builder.Property(e => e.MinOrderAmount)
                .HasPrecision(10, 2);

            builder.HasOne ( e => e.ApplicableMovie)
                .WithMany()
                .HasForeignKey(e => e.ApplicableMovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ApplicableBranch)
                .WithMany()
                .HasForeignKey(e => e.ApplicableBranchId);

            builder.HasMany(e => e.CouponRedemptions)
                .WithOne( e => e.Coupon)
                .HasForeignKey(e => e.CouponId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
