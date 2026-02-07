namespace TicketBooking.Models.ViewModels {
	public class NavigationMenuViewModel {
		public IEnumerable<string> Locations { get; set; } = Enumerable.Empty<string>();
		public string? CurrentLocation { get; set; }
	}
}
