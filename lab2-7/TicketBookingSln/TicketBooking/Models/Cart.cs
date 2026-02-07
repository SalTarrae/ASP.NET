namespace TicketBooking.Models {
	public class Cart {
		public List<CartLine> Lines { get; set; } = new();

		public void AddItem(Event ev, int quantity) {
			var line = Lines.FirstOrDefault(l => l.Event.EventId == ev.EventId);
			if (line == null) {
				Lines.Add(new CartLine { Event = ev, Quantity = quantity });
			} else {
				line.Quantity += quantity;
			}
		}

		public void RemoveLine(long eventId) {
			Lines.RemoveAll(l => l.Event.EventId == eventId);
		}

		public void Clear() => Lines.Clear();

		public decimal ComputeTotalValue()
			=> Lines.Sum(l => l.Event.BaseTicketPrice * l.Quantity);
	}
}
