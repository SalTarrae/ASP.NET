using Microsoft.EntityFrameworkCore;
using TicketBooking.Data.Models;

namespace TicketBooking.Data.Contexts {
	public class TicketBookingDbContext : DbContext {
		public TicketBookingDbContext(DbContextOptions<TicketBookingDbContext> options)
			: base(options) { }

		public DbSet<Event> Events => Set<Event>();
		public DbSet<Booking> Bookings => Set<Booking>();

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Booking>()
				.HasOne(b => b.Event)
				.WithMany(e => e.Bookings)
				.HasForeignKey(b => b.EventId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
