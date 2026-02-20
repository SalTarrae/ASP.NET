using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Data.Models {
	public class Booking {
		public long BookingId { get; set; }

		[Required(ErrorMessage = "Please enter customer name")]
		[StringLength(100, ErrorMessage = "Name must be up to 100 characters")]
		public string CustomerName { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter an email")]
		[EmailAddress(ErrorMessage = "Please enter a valid email address")]
		[StringLength(120)]
		public string CustomerEmail { get; set; } = string.Empty;

		[Required]
		[Range(1, 10, ErrorMessage = "Quantity must be between 1 and 10")]
		public int Quantity { get; set; } = 1;

		// FK -> Event
		[Range(1, long.MaxValue, ErrorMessage = "Please select an event")]  // This ensures that the EventId must be a positive number, which is important for a valid foreign key reference.
		public long EventId { get; set; }

		public Event? Event { get; set; }

		public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
	}
}
