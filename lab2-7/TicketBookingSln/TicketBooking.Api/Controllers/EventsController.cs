using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Api.DTOs;
using TicketBooking.Api.Mapping;
using TicketBooking.Data.Models;
using TicketBooking.Data.Repositories;

namespace TicketBooking.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class EventsController : ControllerBase {
		private readonly IEventRepository _repo;

		public EventsController(IEventRepository repo) => _repo = repo;

		[HttpGet]
		public IActionResult GetAll() {
			var events = _repo.Events
				.OrderBy(ev => ev.EventId)
				.Select(ev => new EventDto {
					EventId = ev.EventId ?? 0,
					Title = ev.Title,
					Description = ev.Description,
					Location = ev.Location,
					StartDate = ev.StartDate,
					BaseTicketPrice = ev.BaseTicketPrice,
					BookingsCount = ev.Bookings.Count
				})
				.ToList();

			return Ok(events);
		}

		[HttpGet("{id:long}")]
		public IActionResult Get(long id) {
			var ev = _repo.Events.FirstOrDefault(item => item.EventId == id);
			return ev == null ? NotFound() : Ok(ev.ToDto());
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Create([FromBody] EventCreateUpdateDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var ev = new Event {
				Title = dto.Title,
				Description = dto.Description,
				Location = dto.Location,
				StartDate = dto.StartDate,
				BaseTicketPrice = dto.BaseTicketPrice
			};

			_repo.CreateEvent(ev);

			return CreatedAtAction(nameof(Get), new { id = ev.EventId ?? 0 }, ev.ToDto());
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("{id:long}")]
		public IActionResult Update(long id, [FromBody] EventCreateUpdateDto dto) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var ev = _repo.Events.FirstOrDefault(item => (item.EventId ?? 0) == id);
			if (ev == null)
				return NotFound();

			ev.Title = dto.Title;
			ev.Description = dto.Description;
			ev.Location = dto.Location;
			ev.StartDate = dto.StartDate;
			ev.BaseTicketPrice = dto.BaseTicketPrice;

			_repo.UpdateEvent(ev);
			return NoContent();
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("{id:long}")]
		public IActionResult Delete(long id) {
            var ev = _repo.Events.FirstOrDefault(item => (item.EventId ?? 0) == id);
            if (ev == null) return NotFound();

            _repo.DeleteEvent(ev);
            return NoContent();
		}
	}
}
