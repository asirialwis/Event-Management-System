using Event_Management_System_Backend.Dtos.Attendee;
using Event_Management_System_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System_Backend.Controllers
{
    [ApiController]
    [Route("api/attendee")]
    public class AttendeeController : ControllerBase
    {
        

        private readonly IAttendeeService _attendeeService;


        public AttendeeController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }


        [HttpPut("{eventId}/update/{attendeeId}")]
        public async Task<IActionResult> UpdateAttendee(int eventId, int attendeeId, [FromBody] UpdateAttendeeDto attendeeDto)
        {
            if (attendeeDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _attendeeService.UpdateAttendeeAsync(eventId, attendeeId, attendeeDto);

            if (result == "Event not found!" || result == "Attendee not found in this event!")
            {
                return NotFound(result);
            }

            return Ok(result);
        }


    }
}
