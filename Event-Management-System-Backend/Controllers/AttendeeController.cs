using Event_Management_System_Backend.Dtos.Attendee;
using Event_Management_System_Backend.Interfaces;
using Event_Management_System_Backend.Services;
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


        // Add an attendee for specific evend
        [HttpPost("add-attendee/{eventId}")]
        public async Task<IActionResult> AddAttendee(int eventId, [FromBody] AddAttendeeDto addAttendeeDto)
        {
            if (addAttendeeDto == null)
                return BadRequest("Attendee data is required.");

            var result = await _attendeeService.AddAttendeeAsync(eventId, addAttendeeDto);



            return Ok("Attendee added successfully.");
        }

        //update assigned attendee data
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


        //delete assigned attendee
        [HttpDelete("{eventId}/attendees/{attendeeId}")]
        public async Task<IActionResult> DeleteAttendee(int eventId, int attendeeId)
        {
            var result = await _attendeeService.DeleteAttendeeAsync(eventId, attendeeId);

            if (result == "Event not found!" || result == "Attendee not found!")
            {
                return NotFound(result);
            }

            return Ok(result);
        }


    }
}
