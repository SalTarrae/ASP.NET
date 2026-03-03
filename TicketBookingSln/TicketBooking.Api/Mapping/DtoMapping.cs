using TicketBooking.Api.DTOs;
using TicketBooking.Data.Models;

namespace TicketBooking.Api.Mapping {
	public static class DtoMapping {
		public static EventDto ToDto(this Event ev) => new() {
			EventId = ev.EventId ?? 0,
			Title = ev.Title,
			Description = ev.Description,
			Location = ev.Location,
			StartDate = ev.StartDate,
			BaseTicketPrice = ev.BaseTicketPrice,
			BookingsCount = ev.Bookings?.Count ?? 0
		};

		public static BookingDto ToDto(this Booking booking) => new() {
			BookingId = booking.BookingId,
			CustomerName = booking.CustomerName,
			CustomerEmail = booking.CustomerEmail,
			Quantity = booking.Quantity,
			EventId = booking.EventId,
			CreatedAtUtc = booking.CreatedAtUtc,
			EventTitle = booking.Event?.Title ?? "",
			EventStartDate = booking.Event?.StartDate ?? default,
			EventLocation = booking.Event?.Location ?? ""
		};
	}
}
