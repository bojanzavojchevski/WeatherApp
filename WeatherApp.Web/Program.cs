using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Domain.IdentityModels;
using WeatherApp.Repository.Data;
using WeatherApp.Repository.Implementations;
using WeatherApp.Repository.Interfaces;
using WeatherApp.Service.Implementations;
using WeatherApp.Service.Interfaces;
using WeatherApp.Web.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<WeatherAppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();


builder.Services.AddScoped<IFavoriteLocationRepository, FavoriteLocationRepository>();
builder.Services.AddScoped<IFavoriteLocationService, FavoriteLocationService>();

builder.Services.AddScoped<IWeatherSnapshotRepository, WeatherSnapshotRepository>();
builder.Services.AddScoped<IWeatherSnapshotService, WeatherSnapshotService>();

builder.Services.AddScoped<IAlertRuleRepository, AlertRuleRepository>();
builder.Services.AddScoped<IAlertRuleService, AlertRuleService>();

builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddHttpClient("OpenWeather", client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
});
builder.Services.AddScoped<OpenWeatherService>();

//builder.Services.AddHttpClient<ChatGPTService>(client =>
//{
//    client.BaseAddress = new Uri("https://api.openai.com/");
//});
//builder.Services.AddScoped<ChatGPTService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Home/AccessDenied");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WeatherSnapshots}/{action=FetchWeather}/{id?}");
app.MapRazorPages();

await SeedRolesAndAdminAsync(app.Services);


app.Run();




async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<WeatherAppUser>>();

    // Step 1: Ensure roles exist
    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Step 2: Seed default Admin user
    string adminEmail = "admin@weatherapp.com";
    string adminPassword = "Admin123!"; //

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new WeatherAppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}