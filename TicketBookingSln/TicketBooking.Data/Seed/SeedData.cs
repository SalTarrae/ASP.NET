using Microsoft.EntityFrameworkCore;
using TicketBooking.Data.Contexts;
using TicketBooking.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TicketBooking.Data.Seed {
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

			if (!context.Bookings.Any()) {
				var firstEvent = context.Events.OrderBy(e => e.EventId).FirstOrDefault();
				var secondEvent = context.Events.OrderBy(e => e.EventId).Skip(1).FirstOrDefault();
				var thirdEvent = context.Events.OrderBy(e => e.EventId).Skip(2).FirstOrDefault();

				var bookings = new List<Booking>();

				if (firstEvent != null) {
					bookings.AddRange(new[] {
						new Booking
						{
							CustomerName = "Denys Test",
							CustomerEmail = "denys.test@example.com",
							Quantity = 2,
							EventId = firstEvent.EventId ?? 0
						},
						new Booking
						{
							CustomerName = "Anna Test",
							CustomerEmail = "anna.test@example.com",
							Quantity = 1,
							EventId = firstEvent.EventId ?? 0
						}
					});
				}

				if (secondEvent != null) {
					bookings.Add(new Booking {
						CustomerName = "Ivan Test",
						CustomerEmail = "ivan.test@example.com",
						Quantity = 3,
						EventId = secondEvent.EventId ?? 0
					});
				}

				if (thirdEvent != null) {
					bookings.Add(new Booking {
						CustomerName = "Olha Test",
						CustomerEmail = "olha.test@example.com",
						Quantity = 1,
						EventId = thirdEvent.EventId ?? 0
					});
				}

				if (bookings.Count > 0) {
					context.Bookings.AddRange(bookings);
					context.SaveChanges();
				}
			}
		}
	}
}
