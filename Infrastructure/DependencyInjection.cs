using Infrastructure.Persistence.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure ( this IServiceCollection services , IConfiguration configuration )
        {
            services.AddDbContext<AppDbContext>(options =>options
                .UseSqlServer( configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication();

            services.AddAuthorization();

            return services;
        }
    }
}
