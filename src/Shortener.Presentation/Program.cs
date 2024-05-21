using Shortener.Presentation.Tools;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddControllersWithViews();

builder.Services.AddServicesIntoDI();
builder.Services.AddAutoMapper();
builder.Services.AddCorsPolicies();
builder.Services.ConfigureEntityFrameworkCore(configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAuthentication(configuration);
builder.Services.AddSwagger();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseCors("AllowAll");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<RoleInitializerMiddlwere>();
app.MapDefaultControllerRoute();

app.Run();
