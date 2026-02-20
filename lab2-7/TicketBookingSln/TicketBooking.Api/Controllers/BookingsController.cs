using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Api.DTOs;
using TicketBooking.Api.Mapping;
using TicketBooking.Data.Models;
using TicketBooking.Data.Repositories;

namespace TicketBooking.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class BookingsController : ControllerBase {
		private readonly IBookingRepository _repo;
		private readonly IEventRepository _eventRepo;

		public BookingsController(IBookingRepository repo, IEventRepository eventRepo) {
			_repo = repo;
			_eventRepo = eventRepo;
		}

		[HttpGet]
		public IActionResult GetAll() {
			var bookings = _repo.Bookings
				.OrderBy(booking => booking.BookingId)
				.Select(booking => new BookingDto {
					BookingId = booking.BookingId,
					CustomerName = booking.CustomerName,
					CustomerEmail = booking.CustomerEmail,
					Quantity = booking.Quantity,
					EventId = booking.EventId,
					CreatedAtUtc = booking.CreatedAtUtc,
					EventTitle = booking.Event!.Title,
					EventStartDate = booking.Event!.StartDate,
					EventLocation = booking.Event!.Location
				})
				.ToList();

			return Ok(bookings);
		}

		[HttpGet("{id:long}")]
		public IActionResult Get(long id) {
			var booking = _repo.Bookings.FirstOrDefault(item => item.BookingId == id);
			return booking == null ? NotFound() : Ok(booking.ToDto());
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Create([FromBody] BookingCreateUpdateDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var ev = _eventRepo.Events.FirstOrDefault(item => (item.EventId ?? 0) == dto.EventId);
			if (ev == null)
				return BadRequest("Event does not exist");

			var booking = new Booking {
				CustomerName = dto.CustomerName,
				CustomerEmail = dto.CustomerEmail,
				Quantity = dto.Quantity,
				EventId = dto.EventId,
				CreatedAtUtc = DateTime.UtcNow
			};

			_repo.CreateBooking(booking);

			var created = _repo.Bookings.First(b => b.BookingId == booking.BookingId);
			return CreatedAtAction(nameof(Get), new { id = booking.BookingId }, created.ToDto());
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("{id:long}")]
		public IActionResult Update(long id, [FromBody] BookingCreateUpdateDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var booking = _repo.Bookings.FirstOrDefault(b => b.BookingId == id);
			if (booking == null)
				return NotFound();

			var ev = _eventRepo.Events.FirstOrDefault(e => (e.EventId ?? 0) == dto.EventId);
			if (ev == null)
				return BadRequest("Event does not exist");

			booking.CustomerName = dto.CustomerName;
			booking.CustomerEmail = dto.CustomerEmail;
			booking.Quantity = dto.Quantity;
			booking.EventId = dto.EventId;

			_repo.UpdateBooking(booking);
			return NoContent();
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("{id:long}")]
		public IActionResult Delete(long id) {
			var booking = _repo.Bookings.FirstOrDefault(item => item.BookingId == id);
			if (booking == null)
				return NotFound();

			_repo.DeleteBooking(booking);
			return NoContent();
		}
	}
}
