// Add these using statements at the top
using Microsoft.EntityFrameworkCore;
using MostafaEidPortfolio.Data; // Make sure this matches your project's namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// This is where you register things like your database context.
builder.Services.AddControllersWithViews();

// --- THIS IS THE DATABASE REGISTRATION ---
// This tells your application how to connect to the SQL database.
// It reads the connection string from your appsettings.json file (or from Azure's configuration).
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
// This is middleware that handles every request to your website.
if (!app.Environment.IsDevelopment())
{
    // If there's an unhandled error, redirect to a friendly error page.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Adds security headers (good for production)
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS
app.UseStaticFiles();       // Serves static files like CSS, JS, and images
app.UseRouting();             // Enables URL routing (e.g., /Home/About)
app.UseAuthorization();          // Enables login/authentication features

// Defines the default URL pattern for your site.
// {controller=Home}/{action=Index}/{id?} means:
// / -> goes to HomeController.Index()
// /Home/About -> goes to HomeController.About()
// /Home/Details/5 -> goes to HomeController.Details(5)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Starts the web server and begins listening for requests