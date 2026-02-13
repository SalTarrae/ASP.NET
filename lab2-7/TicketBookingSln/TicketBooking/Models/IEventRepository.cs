namespace TicketBooking.Models {
	public interface IEventRepository {
		IQueryable<Event> Events { get; }

		void CreateEvent(Event e);
		void UpdateEvent(Event e);
		void DeleteEvent(Event e);
	}
}
