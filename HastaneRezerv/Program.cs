using HastaneRezerv.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configure services and add them to the container.
builder.Services.AddDbContext<HastaneContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("YourConnection")));

builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    // Extend the session timeout to a more reasonable value, e.g., 20 minutes.
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login"; // Giriş sayfasının yolu
        // Erişim reddedildi sayfasının yolu
    });

builder.Services.AddIdentity<RegisterModelcs, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<HastaneContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Ensure roles are created and create a default user
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<RegisterModelcs>>();

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use a more informative error handling page during production.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SignIn}/{action=SignIn}/{id?}");

app.Run();

// Helper method to create roles

