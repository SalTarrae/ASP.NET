using Microsoft.EntityFrameworkCore;
using TicketBooking.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<TicketBookingDbContext>(opts => {
	opts.UseSqlServer(builder.Configuration["ConnectionStrings:TicketBookingConnection"]);
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddScoped<IEventRepository, EFEventRepository>();
builder.Services.AddScoped<IBookingRepository, EFBookingRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.MapDefaultControllerRoute();
SeedData.EnsurePopulated(app);
app.Run();