using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class ReviewConfiguration
    : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Rating)
            .IsRequired();

        builder.Property(e => e.Comment)
            .HasMaxLength(1000);

        builder.Property(e => e.IsApproved)
            .IsRequired();

        builder.HasOne(e => e.User)
            .WithMany(e => e.Reviews)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Movie)
            .WithMany(e => e.Reviews)
            .HasForeignKey(e => e.MovieId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new
        {
            e.UserId,
            e.MovieId
        })
        .IsUnique();
    }
}