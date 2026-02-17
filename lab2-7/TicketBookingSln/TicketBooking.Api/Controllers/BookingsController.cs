using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Data.Models;
using TicketBooking.Data.Repositories;

namespace TicketBooking.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class BookingsController : ControllerBase {
		private readonly IBookingRepository _repo;

		public BookingsController(IBookingRepository repo) => _repo = repo;

		[HttpGet]
		public IActionResult GetAll()
			=> Ok(_repo.Bookings.OrderBy(b => b.BookingId).ToList());

		[HttpGet("{id:long}")]
		public IActionResult Get(long id) {
			var b = _repo.Bookings.FirstOrDefault(x => x.BookingId == id);
			return b == null ? NotFound() : Ok(b);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Create([FromBody] Booking model) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			_repo.CreateBooking(model);
			return CreatedAtAction(nameof(Get), new { id = model.BookingId }, model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("{id:long}")]
		public IActionResult Update(long id, [FromBody] Booking model) {
			if (id != model.BookingId)
				return BadRequest("Id mismatch");
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_repo.UpdateBooking(model);
			return NoContent();
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("{id:long}")]
		public IActionResult Delete(long id) {
			var b = _repo.Bookings.FirstOrDefault(x => x.BookingId == id);
			if (b == null)
				return NotFound();

			_repo.DeleteBooking(b);
			return NoContent();
		}
	}
}
