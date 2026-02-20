using System.ComponentModel.DataAnnotations;

namespace TicketBooking.Api.DTOs {
	public class EventCreateUpdateDto {
		[Required, StringLength(120)]
		public string Title { get; set; } = "";

		[Required, StringLength(1000)]
		public string Description { get; set; } = "";

		[Required, StringLength(80)]
		public string Location { get; set; } = "";

		[Required]
		public DateTime StartDate { get; set; }

		[Range(0.01, 10000000)]
		public decimal BaseTicketPrice { get; set; }
	}
}
