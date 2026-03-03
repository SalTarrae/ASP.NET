using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Data.Models;
using TicketBooking.Data.Repositories;

namespace TicketBooking.Controllers {
	[Authorize(Roles = "Admin")]
	public class EventController : Controller {
		private readonly IEventRepository _repo;

		public EventController(IEventRepository repo) {
			_repo = repo;
		}

		[AllowAnonymous]
		public IActionResult Index() {
			var events = _repo.Events.OrderBy(e => e.EventId).ToList();
			return View(events);
		}

		[AllowAnonymous]
		public IActionResult Details(long id) {
			var ev = _repo.Events.FirstOrDefault(e => e.EventId == id);
			if (ev == null)
				return NotFound();
			return View(ev);
		}

		[Authorize]
		[HttpGet]
		public IActionResult Create() {
			return View(new Event());
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Event model) {
			if (!ModelState.IsValid)
				return View(model);

			_repo.CreateEvent(model);
			return RedirectToAction(nameof(Index));
		}

		[Authorize]
		[HttpGet]
		public IActionResult Edit(long id) {
			var ev = _repo.Events.FirstOrDefault(e => e.EventId == id);
			if (ev == null)
				return NotFound();
			return View(ev);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Event model) {
			if (!ModelState.IsValid)
				return View(model);

			_repo.UpdateEvent(model);
			return RedirectToAction(nameof(Index));
		}

		[Authorize]
		[HttpGet]
		public IActionResult Delete(long id) {
			var ev = _repo.Events.FirstOrDefault(e => e.EventId == id);
			if (ev == null)
				return NotFound();
			return View(ev);
		}

		[Authorize]
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
