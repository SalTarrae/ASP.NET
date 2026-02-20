namespace TicketBooking.Api.DTOs {
	public class BookingDto {
		public long BookingId { get; set; }
		public string CustomerName { get; set; } = "";
		public string CustomerEmail { get; set; } = "";
		public int Quantity { get; set; }
		public long EventId { get; set; }
		public DateTime CreatedAtUtc { get; set; }

		public string EventTitle { get; set; } = "";
		public DateTime EventStartDate { get; set; }
		public string EventLocation { get; set; } = "";
	}
}
