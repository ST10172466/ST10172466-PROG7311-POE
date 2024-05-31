using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PROG7311_POE_Part_2.Classes;

var builder = WebApplication.CreateBuilder(args);

//-----------------------------------------------------------------------------------------------//
// Authentication adds cookies to authorize the User to see the navbar upon login
// When User closes browser, cookies are cleared, and navbar will not be displayed
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(option =>
	{
		option.LoginPath = "/Login/LoginView";
		option.ExpireTimeSpan = TimeSpan.FromMinutes(15); // Set the cookie to be a session cookie
		option.SlidingExpiration = false; // Disable sliding expiration
	});

//-----------------------------------------------------------------------------------------------//
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContextClass>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("connectionString")));

// Ensure the DbContextOptions<DatabaseContextClass> is passed to the constructor of DatabaseContextClass
builder.Services.AddScoped<DatabaseContextClass>();

var app = builder.Build();

//-----------------------------------------------------------------------------------------------//
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Shared/Error");
	app.UseHsts();
}

//-----------------------------------------------------------------------------------------------//
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//-----------------------------------------------------------------------------------------------//
app.MapControllerRoute(
name: "login",
pattern: "{controller=Login}/{action=LoginView}/{id?}");

//-----------------------------------------------------------------------------------------------//
app.MapRazorPages();
app.Run();