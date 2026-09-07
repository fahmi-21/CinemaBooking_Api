using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure( EntityTypeBuilder<Notification> builder ) 
        {
            builder.HasKey( x => x.Id );

            builder.Property(e => e.Type)
                .IsRequired();

            builder.Property(e=>e.Title)
                .IsRequired()
                .HasMaxLength( 100 );

            builder.Property(e => e.Body)
            .HasMaxLength(1000);

            builder.HasOne(e => e.User)
                .WithMany(e => e.Notifications)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
