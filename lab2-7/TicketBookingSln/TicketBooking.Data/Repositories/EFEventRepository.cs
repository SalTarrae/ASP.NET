using TicketBooking.Data.Contexts;
using TicketBooking.Data.Models;

namespace TicketBooking.Data.Repositories {
	public class EFEventRepository : IEventRepository {
		private readonly TicketBookingDbContext _context;

		public EFEventRepository(TicketBookingDbContext context) {
			_context = context;
		}

		public IQueryable<Event> Events => _context.Events;

		public void CreateEvent(Event ev) {
			_context.Events.Add(ev);
			_context.SaveChanges();
		}

		public void UpdateEvent(Event ev) {
			_context.Events.Update(ev);
			_context.SaveChanges();
		}

		public void DeleteEvent(Event ev) {
			_context.Events.Remove(ev);
			_context.SaveChanges();
		}
	}
}
