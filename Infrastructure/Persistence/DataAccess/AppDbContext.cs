using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.Persistence;

namespace Infrastructure.Persistence.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingSeat> BookingSeats => Set<BookingSeat>();
        public DbSet<Coupon> Coupons => Set<Coupon>();
        public DbSet<CouponRedemption> CouponRedemptions => Set<CouponRedemption>();
        public DbSet<Branch> Branches => Set<Branch>();
        public DbSet<Favorite> Favorites => Set<Favorite>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Hall> Halls => Set<Hall>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<MovieGenre> MovieGenres => Set<MovieGenre>();
        public DbSet<MovieActor> MovieActors => Set<MovieActor>();
        public DbSet<Notification> Notifications => Set<Notification>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Seat> Seats => Set<Seat>();
        public DbSet<Section> Sections => Set<Section>();
        public DbSet<Showtime> Showtimes => Set<Showtime>();
        public DbSet<ShowtimeSeat> ShowtimeSeats => Set<ShowtimeSeat>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Ticket> Tickets => Set<Ticket>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
