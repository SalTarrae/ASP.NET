using Microsoft.EntityFrameworkCore;
using TicketBooking.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TicketBookingDbContext>(opts => {
	opts.UseSqlServer(builder.Configuration["ConnectionStrings:TicketBookingConnection"]);
});

builder.Services.AddScoped<IEventRepository, EFEventRepository>();

var app = builder.Build();
app.UseStaticFiles();
app.MapDefaultControllerRoute();
SeedData.EnsurePopulated(app);
app.Run();