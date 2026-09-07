using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class SectionConfiguration
    : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.DisplayOrder)
            .IsRequired();

        builder.HasOne(e => e.Hall)
            .WithMany(e => e.Sections)
            .HasForeignKey(e => e.HallId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Seats)
            .WithOne(e => e.Section)
            .HasForeignKey(e => e.SectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => new
        {
            e.HallId,
            e.Name
        })
        .IsUnique();

        builder.HasIndex(e => new
        {
            e.HallId,
            e.DisplayOrder
        })
        .IsUnique();
    }
}