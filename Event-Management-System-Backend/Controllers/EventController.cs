using Event_Management_System_Backend.Data;
using Event_Management_System_Backend.Dtos.Attendee;
using Event_Management_System_Backend.Dtos.Event;
using Event_Management_System_Backend.Interfaces;
using Event_Management_System_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System_Backend.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;

        }



        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
        {
            if (eventDto == null)
            {
                return BadRequest("Event data is null.");
            }

            var createdEvent = await _eventService.CreateEventAsync(eventDto);

            return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventDetail = await _eventService.GetEventByIdAsync(id);
            if (eventDetail == null)
            {
                return NotFound();
            }

            return Ok(eventDetail);
        }


        // GET: api/event/getall
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<EventDetailDto>>> GetAllEvents()
        {
            try
            {
                var events = await _eventService.GetAllEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving events");
            }
        }



        // POST: api/event/{eventId}/attendee
        [HttpPost("{eventId}/attendee")]
        public async Task<IActionResult> AddAttendee(int eventId, [FromBody] AttendeeDto attendeeDto)
        {
            if (attendeeDto == null)
                return BadRequest("Attendee data is required.");

            var result = await _eventService.AddAttendeeAsync(eventId, attendeeDto);

            

            return Ok("Attendee added successfully.");
        }
    }
}
