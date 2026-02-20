using Microsoft.EntityFrameworkCore;
using TicketBooking.Data.Contexts;
using TicketBooking.Data.Models;

namespace TicketBooking.Data.Repositories {
	public class EFBookingRepository : IBookingRepository {
		private readonly TicketBookingDbContext _context;

		public EFBookingRepository(TicketBookingDbContext context) {
			_context = context;
		}

		public IQueryable<Booking> Bookings =>
			_context.Bookings.Include(booking => booking.Event);

		public void CreateBooking(Booking booking) {
			_context.Bookings.Add(booking);
			_context.SaveChanges();
		}

		public void UpdateBooking(Booking booking) {
			_context.Bookings.Update(booking);
			_context.SaveChanges();
		}

		public void DeleteBooking(Booking booking) {
			_context.Bookings.Remove(booking);
			_context.SaveChanges();
		}
	}
}
