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



        [HttpPut("update-event/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventUpdateDto eventUpdateDto)
        {
            var result = await _eventService.UpdateEventDetailsAsync(id, eventUpdateDto);

            if (result == "Event updated successfully!")
            {
                return Ok(result);
            }

            return NotFound(result);
        }


        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            var result = await _eventService.DeleteEventAsync(eventId);

            if (result == "Event not found!")
            {
                return NotFound(result);
            }

            return Ok(result);
        }





    }
}
