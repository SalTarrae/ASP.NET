using Microsoft.AspNetCore.Mvc;
using TicketBooking.Models;

namespace TicketBooking.Controllers {
	public class EventController : Controller {
		private readonly IEventRepository _repo;

		public EventController(IEventRepository repo) {
			_repo = repo;
		}

		public IActionResult Index() {
			var events = _repo.Events.OrderBy(e => e.EventId).ToList();
			return View(events);
		}

		public IActionResult Details(long id) {
			var ev = _repo.Events.FirstOrDefault(e => e.EventId == id);
			if (ev == null)
				return NotFound();
			return View(ev);
		}

		[HttpGet]
		public IActionResult Create() {
			return View(new Event());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Event model) {
			if (!ModelState.IsValid)
				return View(model);

			_repo.CreateEvent(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Edit(long id) {
			var ev = _repo.Events.FirstOrDefault(e => e.EventId == id);
			if (ev == null)
				return NotFound();
			return View(ev);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Event model) {
			if (!ModelState.IsValid)
				return View(model);

			_repo.UpdateEvent(model);
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public IActionResult Delete(long id) {
			var ev = _repo.Events.FirstOrDefault(e => e.EventId == id);
			if (ev == null)
				return NotFound();
			return View(ev);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(long id) {
			var ev = _repo.Events.FirstOrDefault(e => e.EventId == id);
			if (ev == null)
				return NotFound();

			_repo.DeleteEvent(ev);
			return RedirectToAction(nameof(Index));
		}
	}
}
