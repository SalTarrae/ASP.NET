using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketBooking.Infrastructure;

namespace TicketBooking.Models {
	public class Event {
		public long? EventId { get; set; }
		
        [Required(ErrorMessage = "Please enter a title")]
        [StringLength(120, ErrorMessage = "Title must be up to 120 characters")]
		public string Title { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter a description")]
		[StringLength(1000, ErrorMessage = "Description must be up to 1000 characters")]
		public string Description { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please enter a location")]
		[StringLength(80, ErrorMessage = "Location must be up to 80 characters")]
		public string Location { get; set; } = string.Empty;

		[Required(ErrorMessage = "Please select a date and time")]
		[FutureDate(ErrorMessage = "Start date must be in the future")]
		public DateTime StartDate { get; set; }

		[Required]
		[Range(0.01, 10000000, ErrorMessage = "Please enter a positive ticket price")]
		[Column(TypeName = "decimal(10, 2)")]
		public decimal BaseTicketPrice { get; set; }

		public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
	}
}
