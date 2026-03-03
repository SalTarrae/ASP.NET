using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketBooking.Data.Models;
using TicketBooking.Data.Repositories;

namespace TicketBooking.Controllers {
	[Authorize(Roles = "Admin")]
	public class BookingController : Controller {
		private readonly IBookingRepository _bookingRepo;
		private readonly IEventRepository _eventRepo;

		public BookingController(IBookingRepository bookingRepo, IEventRepository eventRepo) {
			_bookingRepo = bookingRepo;
			_eventRepo = eventRepo;
		}

		[Authorize]
		public IActionResult Index() {
			var data = _bookingRepo.Bookings.OrderByDescending(b => b.BookingId).ToList();
			return View(data);
		}

		[Authorize]
		public IActionResult Details(long id) {
			var booking = _bookingRepo.Bookings.FirstOrDefault(b => b.BookingId == id);
			if (booking == null)
				return NotFound();
			return View(booking);
		}

		[Authorize]
		[HttpGet]
		public IActionResult Create() {
			ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "EventId", "Title");
			return View(new Booking());
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Booking model) {
			if (!ModelState.IsValid) {
				ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "EventId", "Title", model.EventId);
				return View(model);
			}

			_bookingRepo.CreateBooking(model);
			return RedirectToAction(nameof(Index));
		}

		[Authorize]
		[HttpGet]
		public IActionResult Edit(long id) {
			var booking = _bookingRepo.Bookings.FirstOrDefault(b => b.BookingId == id);
			if (booking == null)
				return NotFound();

			ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "EventId", "Title", booking.EventId);
			return View(booking);
		}

		[Authorize]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Booking model) {
			if (!ModelState.IsValid) {
				ViewBag.Events = new SelectList(_eventRepo.Events.ToList(), "EventId", "Title", model.EventId);
				return View(model);
			}

			_bookingRepo.UpdateBooking(model);
			return RedirectToAction(nameof(Index));
		}

		[Authorize]
		[HttpGet]
		public IActionResult Delete(long id) {
			var booking = _bookingRepo.Bookings.FirstOrDefault(b => b.BookingId == id);
			if (booking == null)
				return NotFound();
			return View(booking);
		}

		[Authorize]
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(long id) {
			var booking = _bookingRepo.Bookings.FirstOrDefault(b => b.BookingId == id);
			if (booking == null)
				return NotFound();

			_bookingRepo.DeleteBooking(booking);
			return RedirectToAction(nameof(Index));
		}
	}
}
