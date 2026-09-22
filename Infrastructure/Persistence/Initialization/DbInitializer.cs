using Application.Abstractions.Persistence;
using Domain.Entities.Identity;
using Infrastructure.Persistence.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistence.Initialization
{
    public class DbInitializer : IDbInitislizer
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;

        public DbInitializer(RoleManager<IdentityRole<Guid>> roleManager,
            UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>(Application.Common.Constants.Roles.SUPER_ADMIN_ROLE));
                await _roleManager.CreateAsync(new IdentityRole<Guid>(Application.Common.Constants.Roles.ADMIN_ROLE));
                await _roleManager.CreateAsync(new IdentityRole<Guid>(Application.Common.Constants.Roles.BRANCH_MANAGER_ROLE));
                await _roleManager.CreateAsync(new IdentityRole<Guid>(Application.Common.Constants.Roles.EMPLOYEE_ROLE));
                await _roleManager.CreateAsync(new IdentityRole<Guid>(Application.Common.Constants.Roles.CUSTOMER_ROLE));
            }
            var admin = await _userManager.FindByEmailAsync("superadmin@project.com");

            if (admin is null)
            {
                await _userManager.CreateAsync(
                    new ApplicationUser
                    {
                        FName = "Super",
                        LName = "Admin",
                        Email = "superadmin@project.com",
                        EmailConfirmed = true,
                        UserName = "SuperAdmin"
                    }, password: "SuperAdmin1234$"
                );
                admin = await _userManager.FindByEmailAsync("superadmin@project.com");
            }

            if (admin is not null)
            {
                await _userManager.AddToRoleAsync(admin, Application.Common.Constants.Roles.SUPER_ADMIN_ROLE);
            }
            
        }

    }
}
