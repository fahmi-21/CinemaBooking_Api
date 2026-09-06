using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.Identity;

namespace Application.Abstractions.Persistence
{
    internal interface IApplicationDbContext
    {
        DbSet<ApplicationUser> Users { get; }
        DbSet<Actor> Actors { get; }
        DbSet<Booking> Bookings { get; }
        DbSet<BookingSeat> BookingSeats { get; }
        DbSet<Coupon> Coupons { get; }
        DbSet<CouponRedemption> CouponRedemptions { get; }
        DbSet<Branch> Branches { get; }
        DbSet<Favorite> Favorites { get; }
        DbSet<Genre> Genres { get; }
        DbSet<Hall> Halls { get; }
        DbSet<Movie> Movies { get; }
        DbSet<MovieGenre> MovieGenres { get; }
        DbSet<MovieActor> MovieActors { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<Review> Reviews { get; }
        DbSet<Seat> Seats { get; }
        DbSet<Section> Sections { get; }
        DbSet<Showtime> Showtimes { get; }
        DbSet<ShowtimeSeat> ShowtimeSeats { get; }
        DbSet<Payment> Payments { get; }
        DbSet<Ticket> Tickets { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
