using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoftwareAssuranceMaturityModel.Infrastructure.Persistence;
using SoftwareAssuranceMaturityModel.Infrastructure.Services;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Actions.User;

namespace SoftwareAssuranceMaturityModel.Infrastructure
{
    public static class DependencyInjection
    {
        private static IServiceCollection AddUserDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDbContext>(options => options
                .UseSqlite(configuration.GetConnectionString("Identity"), migration => migration
                .MigrationsAssembly(typeof(UserDbContext).Assembly.FullName)));
            services.AddScoped<IUserDbContext>(provider => provider
                .GetRequiredService<UserDbContext>());

            return services;
        }

        private static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options
                .UseSqlite(configuration.GetConnectionString("Application"), migration => migration
                .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider => provider
                .GetRequiredService<ApplicationDbContext>());

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddUserDbContext(configuration);
            services.AddApplicationDbContext(configuration);

            services.AddTransient<CreateUser>();

            services.AddScoped<ProtectedLocalStorage>();
            services.AddScoped<ProtectedSessionStorage>();
            services.AddScoped<BrowserStorageService>();

            services.AddScoped<AuthService>();
            services.AddScoped<IAuthService>(provider => provider
                .GetRequiredService<AuthService>());
            services.AddScoped<AuthenticationStateProvider>(provider => provider
                .GetRequiredService<AuthService>());

            return services;
        }
    }
}