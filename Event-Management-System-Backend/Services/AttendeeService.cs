using Event_Management_System_Backend.Data;
using Event_Management_System_Backend.Dtos.Attendee;
using Event_Management_System_Backend.Interfaces;
using Event_Management_System_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System_Backend.Services
{
    public class AttendeeService : IAttendeeService
    {
        private readonly ApplicationDBContext _context;

        public AttendeeService(ApplicationDBContext context)
        {
            _context = context;
        }


        public async Task<string> AddAttendeeAsync(int eventId, AddAttendeeDto addAttendeeDto)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Attendees) // Ensure attendees are loaded
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null)
            {
                return "Event not found.";
            }

            if (eventEntity.RemainingCapacity <= 0)
            {
                return "No remaining capacity for this event.";
            }

            // Check if the attendee already exists based on Email
            var existingAttendee = await _context.Attendees
                .FirstOrDefaultAsync(a => a.Email == addAttendeeDto.Email);

            if (existingAttendee == null)
            {
                // If attendee doesn't exist, create a new one
                existingAttendee = new Attendee
                {
                    Name = addAttendeeDto.Name,
                    Email = addAttendeeDto.Email
                };

                _context.Attendees.Add(existingAttendee);
            }
            else
            {
                // Check if the attendee is already added to this event
                if (eventEntity.Attendees.Any(a => a.Email == existingAttendee.Email))
                {
                    throw new InvalidOperationException("Attendee is already registered for this event.");
                }
            }

            // Add the attendee to the event's Attendees list
            eventEntity.Attendees.Add(existingAttendee);
            eventEntity.RemainingCapacity -= 1;

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



            // Save changes
            await _context.SaveChangesAsync();

            return "Attendee updated successfully!";
        }



        public async Task<string> DeleteAttendeeAsync(int eventId, int attendeeId)
        {
            var existingEvent = await _context.Events
                .Include(e => e.Attendees)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (existingEvent == null)
            {
                return "Event not found!";
            }

            var attendee = existingEvent.Attendees.FirstOrDefault(a => a.Id == attendeeId);

            if (attendee == null)
            {
                return "Attendee not found!";
            }

            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();

            return "Attendee deleted successfully!";
        }


    }
}
