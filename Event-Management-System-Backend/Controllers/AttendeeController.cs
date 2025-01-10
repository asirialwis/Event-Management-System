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

        [HttpPost("add-attendee/{eventId}")]
        public async Task<IActionResult> AddAttendee(int eventId, [FromBody] AddAttendeeDto addAttendeeDto)
        {
            if (string.IsNullOrWhiteSpace(addAttendeeDto.Name))
            {
                return BadRequest(new { message = "Attendee name is required." });
            }

            if (string.IsNullOrWhiteSpace(addAttendeeDto.Email))
            {
                return BadRequest(new { message = "Attendee email is required." });
            }

            try
            {
                var result = await _attendeeService.AddAttendeeAsync(eventId, addAttendeeDto);
                return Ok(new { message = "Attendee added successfully." });
            }
            catch (ArgumentException ex)
            {
                // Handle specific known exceptions
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
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
