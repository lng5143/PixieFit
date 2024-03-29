using PixieFit.Web.Business;
using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Identity;
using PixieFit.Web.Business.Entities;
using PixieFit.Web.Business.Managers;
using Microsoft.AspNetCore.Identity;
using PixieFit.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PFContext>(options
    => options.UseNpgsql(PFContext.ConnectionString));

builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<IPayPalService, PayPalService>();
builder.Services.AddScoped<ICreditManager, CreditManager>();
builder.Services.AddHttpContextAccessor();  

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PFContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseStaticFiles();

app.MapHealthChecks("/healthz");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
