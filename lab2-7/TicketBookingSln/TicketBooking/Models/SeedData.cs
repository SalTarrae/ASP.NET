using Microsoft.EntityFrameworkCore;

namespace TicketBooking.Models {
	public class SeedData {
		public static void EnsurePopulated(IApplicationBuilder app) {
			using var scope = app.ApplicationServices.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<TicketBookingDbContext>();

			if (context.Database.GetPendingMigrations().Any()) {
				context.Database.Migrate();
			}

			if (!context.Events.Any()) {
				context.Events.AddRange(
					new Event {
						Title = "Rock Concert",
						Description = "Live rock concert in the city center",
						Location = "Kyiv, Palace of Sports",
						StartDate = DateTime.Today.AddDays(7).AddHours(19),
						BaseTicketPrice = 650m
					},
					new Event {
						Title = "Stand-up Night",
						Description = "Comedy evening with popular comedians",
						Location = "Lviv, Downtown Club",
						StartDate = DateTime.Today.AddDays(10).AddHours(20),
						BaseTicketPrice = 400m
					},
					new Event {
						Title = "Tech Conference",
						Description = "Talks about .NET, Cloud, and AI",
						Location = "Odesa, Expo Center",
						StartDate = DateTime.Today.AddDays(21).AddHours(9),
						BaseTicketPrice = 1200m
					},
					new Event {
						Title = "Theater Premiere",
						Description = "A new performance premiere",
						Location = "Kharkiv, Drama Theater",
						StartDate = DateTime.Today.AddDays(14).AddHours(18),
						BaseTicketPrice = 500m
					}
				);

				context.SaveChanges();
			}
		}
	}
}
