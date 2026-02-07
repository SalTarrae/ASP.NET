using Microsoft.AspNetCore.Mvc;
using TicketBooking.Models;
using TicketBooking.Models.ViewModels;

namespace TicketBooking.Controllers {
	public class HomeController : Controller {
		private readonly IEventRepository _repository;
		private const int PageSize = 3;

		public HomeController(IEventRepository repository) {
			_repository = repository;
		}

		public IActionResult Index(int pageNum = 1) {
			var query = _repository.Events.OrderBy(e => e.EventId);

			var totalItems = query.Count();

			var eventsOnPage = query
				.Skip((pageNum - 1) * PageSize)
				.Take(PageSize)
				.ToList();
				 
			var model = new EventsListViewModel {
				Events = eventsOnPage,
				PagingInfo = new PagingInfo {
					CurrentPage = pageNum,
					ItemsPerPage = PageSize,
					TotalItems = totalItems
				}
			};

			return View(model);
		}
	}
}