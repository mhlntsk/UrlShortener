using Microsoft.AspNetCore.Identity;
using Shortener.Data.Data;
using Shortener.Data.Entities;

namespace Shortener.Presentation.Tools
{
    public class RoleInitializerMiddlwere : IMiddleware
    {
        private readonly ShortenerDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly IConfiguration configuration; 
        private readonly ILogger<RoleInitializerMiddlwere> logger;
        public RoleInitializerMiddlwere(
            ShortenerDbContext context, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole<int>> roleManager,
            IConfiguration configuration,
            ILogger<RoleInitializerMiddlwere> logger)
        {
            this.configuration = configuration;
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await InitializeAsync(configuration);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
            finally
            {
                await next(context);
            }
        }

        public async Task InitializeAsync(IConfiguration configuration)
        {
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("admin"));
            }

            if (await userManager.FindByEmailAsync(configuration["AdminDefaultEmail"]) == null && !context.Users!.Any())
            {
                var admin = new User 
                { 
                    Email = configuration["AdminDefaultEmail"], 
                    UserName = configuration["AdminDefaultLogin"], 
                    FirstName = configuration["AdminDefaultName"], 
                    LastName = configuration["AdminDefaultSurname"] 
                };

                var result = await userManager.CreateAsync(admin, configuration["AdminDefaultPass"]);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
