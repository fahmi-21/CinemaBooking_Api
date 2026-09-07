using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class MovieActorConfiguration : IEntityTypeConfiguration <MovieActor>
    {
        public void Configure ( EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(e =>e.Id);
            builder.Property( e => e.RoleName)
                .HasMaxLength(200);

            builder.HasOne(e => e.Movie)
                .WithMany(e => e.MovieActors)
                .HasForeignKey(e => e.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Actor)
                .WithMany(e => e.MovieActors)
                .HasForeignKey(e => e.ActorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => new
            {
                e.MovieId,
                e.ActorId
            })
            .IsUnique();

        }
    }
}
