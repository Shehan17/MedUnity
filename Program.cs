using MedUnity.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. Add Authentication Services
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login"; // Where to send users who aren't logged in
        options.AccessDeniedPath = "/AccessDenied"; // Where to send users with wrong permissions
    });

builder.Services.AddRazorPages();

var app = builder.Build();

// 3. Environment Settings
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. IMPORTANT ORDER: Auth must come after Routing and before Map
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// 5. Seed Database right before running
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.SeedData(context);
}

app.Run();