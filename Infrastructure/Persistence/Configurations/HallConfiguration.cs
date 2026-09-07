using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class HallConfiguration : IEntityTypeConfiguration<Domain.Entities.Hall>
    {
        public void Configure ( EntityTypeBuilder <Hall> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Type)
            .IsRequired();

            builder.Property(e => e.Capacity)
           .IsRequired();

            builder.Property(e => e.CleaningBufferMinutes)
            .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(e => e.Branch)
                .WithMany(e => e.Halls)
                .HasForeignKey(e => e.BranchId);

            builder.HasMany(e => e.Sections)
                .WithOne( e => e.Hall)
                .HasForeignKey(e => e.HallId);

            builder.HasMany( e => e.Showtimes)
                .WithOne( e => e.Hall)
                .HasForeignKey (e => e.HallId);
        }
    }
}
