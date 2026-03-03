using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketBooking.Infrastructure;
using TicketBooking.Data.Models;
using TicketBooking.Data.Seed;
using TicketBooking.Data.Contexts;
using TicketBooking.Data.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<TicketBookingDbContext>(opts => {
	opts.UseSqlServer(
		builder.Configuration["ConnectionStrings:TicketBookingConnection"],
		b => b.MigrationsAssembly("TicketBooking")
	);
});

builder.Services.AddDbContext<AppIdentityDbContext>(opts => {
	opts.UseSqlServer(
		builder.Configuration["ConnectionStrings:IdentityConnection"],
		b => b.MigrationsAssembly("TicketBooking")
	);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => {
	options.User.RequireUniqueEmail = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireNonAlphanumeric = false;
})
	.AddEntityFrameworkStores<AppIdentityDbContext>()
	.AddDefaultTokenProviders();

//builder.Services.ConfigureApplicationCookie(options => {
//	options.LoginPath = "/Account/Login";
//	options.AccessDeniedPath = "/Account/AccessDenied";
//});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddScoped<IEventRepository, EFEventRepository>();
builder.Services.AddScoped<IBookingRepository, EFBookingRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);
await IdentitySeedData.EnsurePopulatedAsync(app);

app.Run();