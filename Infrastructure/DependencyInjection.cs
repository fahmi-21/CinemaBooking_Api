using Domain.Entities.Identity;
using Infrastructure.Persistence.DataAccess;
using Infrastructure.Persistence.Initialization;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Abstractions.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure ( this IServiceCollection services , IConfiguration configuration )
        {
            services.AddDbContext<AppDbContext>(options =>options
                .UseSqlServer( configuration.GetConnectionString("DefaultConnection")));

            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IDbInitislizer, DbInitializer>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();


            services.AddAuthentication();

            services.AddAuthorization();

            return services;
        }
    }
}
