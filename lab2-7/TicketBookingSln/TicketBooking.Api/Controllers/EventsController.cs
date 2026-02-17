using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketBooking.Data.Models;
using TicketBooking.Data.Repositories;

namespace TicketBooking.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class EventsController : ControllerBase {
		private readonly IEventRepository _repo;

		public EventsController(IEventRepository repo) => _repo = repo;

		[HttpGet]
		public IActionResult GetAll()
			=> Ok(_repo.Events.OrderBy(e => e.EventId).ToList());

		[HttpGet("{id:long}")]
		public IActionResult Get(long id) {
			var ev = _repo.Events.FirstOrDefault(x => x.EventId == id);
			return ev == null ? NotFound() : Ok(ev);
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult Create([FromBody] Event model) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			_repo.CreateEvent(model);
			return CreatedAtAction(nameof(Get), new { id = model.EventId }, model);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("{id:long}")]
		public IActionResult Update(long id, [FromBody] Event model) {
			if (id != model.EventId)
				return BadRequest("Id mismatch");
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			_repo.UpdateEvent(model);
			return NoContent();
		}

		[Authorize(Roles = "Admin")]
		[HttpDelete("{id:long}")]
		public IActionResult Delete(long id) {
			var ev = _repo.Events.FirstOrDefault(x => x.EventId == id);
			if (ev == null)
				return NotFound();

			_repo.DeleteEvent(ev);
			return NoContent();
		}
	}
}
