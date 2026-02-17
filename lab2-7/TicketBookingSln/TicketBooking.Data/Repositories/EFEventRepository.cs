using TicketBooking.Data.Contexts;
using TicketBooking.Data.Models;

namespace TicketBooking.Data.Repositories {
	public class EFEventRepository : IEventRepository {
		private readonly TicketBookingDbContext _context;

		public EFEventRepository(TicketBookingDbContext context) {
			_context = context;
		}

		public IQueryable<Event> Events => _context.Events;

		public void CreateEvent(Event e) {
			_context.Events.Add(e);
			_context.SaveChanges();
		}

		public void UpdateEvent(Event e) {
			_context.Events.Update(e);
			_context.SaveChanges();
		}

		public void DeleteEvent(Event e) {
			_context.Events.Remove(e);
			_context.SaveChanges();
		}
	}
}
