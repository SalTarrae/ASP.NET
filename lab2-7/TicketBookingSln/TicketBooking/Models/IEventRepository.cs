namespace TicketBooking.Models {
	public interface IEventRepository {
		IQueryable<Event> Events { get; }
	}
}
