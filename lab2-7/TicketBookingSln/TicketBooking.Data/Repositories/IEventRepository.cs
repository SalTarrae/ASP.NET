using TicketBooking.Data.Models;

namespace TicketBooking.Data.Repositories {
	public interface IEventRepository {
		IQueryable<Event> Events { get; }

		void CreateEvent(Event e);
		void UpdateEvent(Event e);
		void DeleteEvent(Event e);
	}
}
