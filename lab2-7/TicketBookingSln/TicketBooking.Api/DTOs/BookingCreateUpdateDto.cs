using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Api.DTOs {
	public class BookingCreateUpdateDto {
		[Required, StringLength(100)]
		public string CustomerName { get; set; } = "";

		[Required, EmailAddress, StringLength(120)]
		public string CustomerEmail { get; set; } = "";

		[Range(1, 10)]
		public int Quantity { get; set; } = 1;

		[Range(1, long.MaxValue, ErrorMessage = "Please select an event")]
		public long EventId { get; set; }
	}
}
