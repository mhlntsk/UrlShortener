using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shortener.Business;
using Shortener.Data.Data;
using Shortener.Data.Entities;
using Shortener.Data.Interfaces;
using Shortener.Data.Repositories;

namespace Shortener.Presentation.Tools
{
    /// <summary>
    /// Used to instance extension-methods to IServiceCollection
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures Entity Framework DbContext for the project.
        /// </summary>
        public static void ConfigureEntityFrameworkCore(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<ShortenerDbContext>(builder =>
                builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Shortener.Presentation")));
        }

        /// <summary>
        /// Configures identity services.
        /// </summary>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(opts =>
            {
                opts.Password.RequireLowercase = true;
                opts.SignIn.RequireConfirmedAccount = false;
                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ShortenerDbContext>()
            .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Configures AutoMapper
        /// </summary>
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DataMapperProfile>();
                cfg.AddProfile<WebMapperProfile>();
            }).CreateMapper());
        }

        /// <summary>
        /// Configures DI-container
        /// </summary>
        public static void AddServicesIntoDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUrlRepository, UrlRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
