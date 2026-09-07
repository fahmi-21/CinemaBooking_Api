using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    internal class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {
        public void Configure ( EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Movie)
           .WithMany(e => e.MovieGenres)
           .HasForeignKey(e => e.MovieId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Genre)
                .WithMany(e => e.MovieGenres)
                .HasForeignKey(e => e.GenreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => new
            {
                e.MovieId,
                e.GenreId
            })
            .IsUnique();

        }
    }
}
