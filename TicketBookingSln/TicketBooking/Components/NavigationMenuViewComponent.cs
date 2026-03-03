using Microsoft.AspNetCore.Mvc;
using TicketBooking.Data.Repositories;
using TicketBooking.Models.ViewModels;

namespace TicketBooking.Components {
	public class NavigationMenuViewComponent : ViewComponent {
		private readonly IEventRepository _repository;

		public NavigationMenuViewComponent(IEventRepository repository) {
			_repository = repository;
		}

		public IViewComponentResult Invoke() {
			string? currentLocation = HttpContext.Request.Query["location"];

			var locations = _repository.Events
				.Select(e => e.Location)
				.Distinct()
				.OrderBy(x => x)
				.ToList();

			var model = new NavigationMenuViewModel {
				Locations = locations,
				CurrentLocation = string.IsNullOrWhiteSpace(currentLocation) ? null : currentLocation
			};

			return View(model);
		}
	}
}
