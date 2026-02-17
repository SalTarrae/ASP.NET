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
			_context.Bookings.Include(b => b.Event);

		public void CreateBooking(Booking b) {
			_context.Bookings.Add(b);
			_context.SaveChanges();
		}

		public void UpdateBooking(Booking b) {
			_context.Bookings.Update(b);
			_context.SaveChanges();
		}

		public void DeleteBooking(Booking b) {
			_context.Bookings.Remove(b);
			_context.SaveChanges();
		}
	}
}
