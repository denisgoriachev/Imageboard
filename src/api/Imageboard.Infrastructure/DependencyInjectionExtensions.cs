using Imageboard.Application;
using Imageboard.Infrastructure.Data;
using Imageboard.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imageboard.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ImageboardDbContext>(options =>
                options.UseLazyLoadingProxies().UseSqlite(configuration.GetConnectionString(nameof(ImageboardDbContext)), 
                sqlite => sqlite.MigrationsAssembly(typeof(ImageboardDbContext).Assembly.FullName))
            );

            services.AddScoped<IImageboardDbContext>(provider => provider.GetService<ImageboardDbContext>());

            services.AddTransient<IFileService, FileService>(provider => new FileService(provider.GetService<IConfiguration>()));
            services.AddTransient<IDateTimeService, DateTimeService>();

            return services;
        }
    }
}
