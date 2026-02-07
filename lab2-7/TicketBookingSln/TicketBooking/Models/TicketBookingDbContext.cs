using Microsoft.EntityFrameworkCore;

namespace TicketBooking.Models {
	public class TicketBookingDbContext : DbContext {
		public TicketBookingDbContext(DbContextOptions<TicketBookingDbContext> options)
			: base(options) { }

		public DbSet<Event> Events => Set<Event>();
	}
}
