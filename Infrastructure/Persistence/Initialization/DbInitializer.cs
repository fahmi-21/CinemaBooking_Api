using Application.Abstractions;
using Domain.Entities.Identity;
using Infrastructure.Persistence.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Initialization
{
    public class DbInitializer : IDbInitislizer
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public DbInitializer(
            RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager,
            AppDbContext context,
            IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();

            var roles = new[]
            {
                Application.Common.Constants.Roles.SUPER_ADMIN_ROLE,
                Application.Common.Constants.Roles.ADMIN_ROLE,
                Application.Common.Constants.Roles.BRANCH_MANAGER_ROLE,
                Application.Common.Constants.Roles.EMPLOYEE_ROLE,
                Application.Common.Constants.Roles.CUSTOMER_ROLE
            };

            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            await EnsureShowtimeSeatInventoryAsync();

            var email = _configuration["BootstrapAdmin:Email"];
            var password = _configuration["BootstrapAdmin:Password"];

            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            var admin = await _userManager.FindByEmailAsync(email);

            if (admin is null)
            {
                admin = new ApplicationUser
                {
                    FName = "Super",
                    LName = "Admin",
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var createResult = await _userManager.CreateAsync(admin, password);

                if (!createResult.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        createResult.Errors.Select(error => error.Description));

                    throw new InvalidOperationException(
                        $"Bootstrap admin creation failed: {errors}");
                }
            }

            var superAdminRole =
                Application.Common.Constants.Roles.SUPER_ADMIN_ROLE;

            if (!await _userManager.IsInRoleAsync(admin, superAdminRole))
            {
                var roleResult =
                    await _userManager.AddToRoleAsync(admin, superAdminRole);

                if (!roleResult.Succeeded)
                {
                    var errors = string.Join(
                        ", ",
                        roleResult.Errors.Select(error => error.Description));

                    throw new InvalidOperationException(
                        $"Bootstrap admin role assignment failed: {errors}");
                }
            }
        }

        private async Task EnsureShowtimeSeatInventoryAsync()
        {
            var missingSeats = await _context.Showtimes
                .AsNoTracking()
                .SelectMany(showtime => _context.Seats
                    .Where(seat =>
                        seat.Section.HallId == showtime.HallId &&
                        !_context.ShowtimeSeats.Any(existing =>
                            existing.ShowtimeId == showtime.Id &&
                            existing.SeatId == seat.Id))
                    .Select(seat => new
                    {
                        ShowtimeId = showtime.Id,
                        SeatId = seat.Id,
                        Price = showtime.BasePrice
                    }))
                .ToListAsync();

            foreach (var missingSeat in missingSeats)
            {
                var showtimeSeat = new Domain.Entities.ShowtimeSeat
                {
                    ShowtimeId = missingSeat.ShowtimeId,
                    SeatId = missingSeat.SeatId,
                    Status = Domain.Enums.ShowtimeSeatStatus.Available,
                    Price = missingSeat.Price
                };

                showtimeSeat.SetCreatedAt();
                _context.ShowtimeSeats.Add(showtimeSeat);
            }

            await _context.SaveChangesAsync();
        }
    }
}