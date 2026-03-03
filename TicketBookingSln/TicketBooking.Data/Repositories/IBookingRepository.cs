using TicketBooking.Data.Models;

namespace TicketBooking.Data.Repositories {
	public interface IBookingRepository {
		IQueryable<Booking> Bookings { get; }

		void CreateBooking(Booking b);
		void UpdateBooking(Booking b);
		void DeleteBooking(Booking b);
	}
}
