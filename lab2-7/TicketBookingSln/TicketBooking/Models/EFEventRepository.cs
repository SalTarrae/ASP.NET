namespace TicketBooking.Models {
	public class EFEventRepository : IEventRepository {
		private readonly TicketBookingDbContext _context;

		public EFEventRepository(TicketBookingDbContext context) {
			_context = context;
		}

		public IQueryable<Event> Events => _context.Events;
	}
}
