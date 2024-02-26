using PixieFit.Web.Business;
using Microsoft.EntityFrameworkCore;
using PixieFit.Web.Identity;
using PixieFit.Web.Business.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PFContext>(options
    => options.UseNpgsql(PFContext.ConnectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PFContext>();

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients);

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
