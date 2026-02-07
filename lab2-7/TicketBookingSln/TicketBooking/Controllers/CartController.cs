using Microsoft.AspNetCore.Mvc;
using TicketBooking.Models;

namespace TicketBooking.Controllers {
	public class CartController : Controller {
		private readonly IEventRepository _repository;
		private readonly Cart _cart;

		public CartController(IEventRepository repository, Cart cart) {
			_repository = repository;
			_cart = cart;
		}

		public IActionResult Index() {
			return View(_cart);
		}

		[HttpPost]
		public IActionResult AddToCart(long eventId, string? returnUrl) {
			var ev = _repository.Events.FirstOrDefault(e => e.EventId == eventId);
			if (ev != null) {
				_cart.AddItem(ev, 1);
				(_cart as SessionCart)?.Save(HttpContext.Session);
			}

			return RedirectToAction("Index", new { returnUrl });
		}

		[HttpPost]
		public IActionResult RemoveFromCart(long eventId, string? returnUrl) {
			_cart.RemoveLine(eventId);
			(_cart as SessionCart)?.Save(HttpContext.Session);

			return RedirectToAction("Index", new { returnUrl });
		}

		[HttpPost]
		public IActionResult Clear(string? returnUrl) {
			_cart.Clear();
			(_cart as SessionCart)?.Save(HttpContext.Session);

			return RedirectToAction("Index", new { returnUrl });
		}
	}
}
