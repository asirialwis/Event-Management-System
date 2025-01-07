using Event_Management_System_Backend.Data;
using Event_Management_System_Backend.Dtos.Attendee;
using Event_Management_System_Backend.Interfaces;
using Event_Management_System_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System_Backend.Services
{
    public class AttendeeService:IAttendeeService
    {
        private readonly ApplicationDBContext _context;

        public AttendeeService(ApplicationDBContext context)
        {
            _context = context;
        }



        public async Task<string> AddAttendeeAsync(int eventId, AddAttendeeDto addAttendeeDto)
        {
            var eventEntity = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null)
            {
                return "Event not found.";
            }

            if (eventEntity.RemainingCapacity <= 0)
            {
                return "No remaining capacity for this event.";
            }

            var attendee = new Attendee
            {
                Name = addAttendeeDto.Name,
                Email = addAttendeeDto.Email
            };

            eventEntity.Attendees.Add(attendee);
            eventEntity.RemainingCapacity -= 1;

            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return "Attendee added successfully.";
        }




        public async Task<string> UpdateAttendeeAsync(int eventId, int attendeeId, UpdateAttendeeDto attendeeDto)
        {
            // Find the event including its attendees
            var existingEvent = await _context.Events
                .Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (existingEvent == null)
            {
                return "Event not found!";
            }

            // Find the specific attendee in the event
            var existingAttendee = existingEvent.Attendees
                .FirstOrDefault(a => a.Id == attendeeId);

            if (existingAttendee == null)
            {
                return "Attendee not found in this event!";
            }

            // Update attendee details
            if (!string.IsNullOrEmpty(attendeeDto.Name))
            {
                existingAttendee.Name = attendeeDto.Name;
            }

            if (!string.IsNullOrEmpty(attendeeDto.Email))
            {
                existingAttendee.Email = attendeeDto.Email;
            }

            // Save changes
            await _context.SaveChangesAsync();

            return "Attendee updated successfully!";
        }


    }
}
