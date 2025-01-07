using Event_Management_System_Backend.Data;
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
    }
}
