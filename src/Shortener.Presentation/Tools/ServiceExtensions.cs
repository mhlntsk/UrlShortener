using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Models.AccountViewModels;
using MVC.Services;
using MVC.Tools.Validation.Account;
using Shortener.Business;
using Shortener.Business.Interfaces;
using Shortener.Business.Models;
using Shortener.Business.Services;
using Shortener.Data.Data;
using Shortener.Data.Entities;
using Shortener.Data.Interfaces;
using Shortener.Data.Repositories;
using Shortener.Presentation.Services;
using Shortener.Presentation.Tools.Validation.Url;

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

            services.AddScoped<IUrlService, UrlService>();

            services.AddSingleton<IValidator<ChangePasswordViewModel>, ChangePasswordViewModelValidator>();
            services.AddSingleton<IValidator<LoginViewModel>, LoginViewModelValidator>();
            services.AddSingleton<IValidator<RegisterViewModel>, RegisterViewModelValidator>();
            services.AddSingleton<IValidator<UrlShortenerModel>, UrlShortenerModelValidator>();

            

            services.AddScoped<RoleInitializerMiddlwere>();
            services.AddScoped<AccountService>();
        }

    }
}
