using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class BranchConfiguration : IEntityTypeConfiguration<Branch>
    { 
        public void Configure (EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey( e => e.Id );

            builder.Property( e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.Address)
           .IsRequired()
           .HasMaxLength(250);

            builder.Property( e => e.GoogleMapsUrl )
                .IsRequired()
                .HasMaxLength(500);
            builder.HasMany(e => e.Halls)
                .WithOne(h => h.Branch)
                .HasForeignKey(h => h.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
