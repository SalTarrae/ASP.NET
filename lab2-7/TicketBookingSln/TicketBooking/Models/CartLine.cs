namespace TicketBooking.Models {
	public class CartLine {
		public Event Event { get; set; } = default!;
		public int Quantity { get; set; }
	}
}
