using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
using System.Text;

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
        public static void ConfigureEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShortenerDbContext>(builder =>
                builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Shortener.Presentation")));
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
        /// Configures authentication with JwtBearer.
        /// </summary>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["shortener_issuer"],
                        ValidAudience = configuration["shortener_audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["my_secret_key"]!))
                    };
                });
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
        /// Configures Swagger
        /// </summary>
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }


/// <summary>
/// Uses to set up Cors policy
/// </summary>
public static void AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });
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
            services.AddScoped<JwtTokenService>();
            services.AddScoped<UrlCasttService>();
        }

    }
}
