namespace TicketBooking.Api.DTOs {
	public class EventDto {
		public long EventId { get; set; }
		public string Title { get; set; } = "";
		public string Description { get; set; } = "";
		public string Location { get; set; } = "";
		public DateTime StartDate { get; set; }
		public decimal BaseTicketPrice { get; set; }

		public int BookingsCount { get; set; }
	}
}
