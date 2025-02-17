using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configurăm baza de date
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurăm autentificarea cu cookie-uri (fără Identity)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSession();  // Activăm sesiunea

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication(); // Activăm autentificarea cu cookie-uri
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
