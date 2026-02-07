using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBooking.Models {
	public class Event {
		public long? EventId { get; set; }

		public string Title { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public DateTime StartDate { get; set; }

		public string Location { get; set; } = string.Empty;

		[Range(0, double.MaxValue, ErrorMessage = "Ticket's price can't be negative")]
		[Column(TypeName = "decimal(10, 2)")]   // 99999999.99 max value
		public decimal BaseTicketPrice { get; set; }
	}
}
