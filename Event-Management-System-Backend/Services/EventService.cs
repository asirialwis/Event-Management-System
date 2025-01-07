using Event_Management_System_Backend.Data;
using Event_Management_System_Backend.Dtos.Attendee;
using Event_Management_System_Backend.Dtos.Event;
using Event_Management_System_Backend.Interfaces;
using Event_Management_System_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System_Backend.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDBContext _context;

        public EventService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateEventAsync(EventDto eventDto)
        {
            var newEvent = new Event
            {
                Name = eventDto.Name,
                Description = eventDto.Description,
                Date = eventDto.Date,
                Location = eventDto.Location,
                CreatedBy = eventDto.CreatedBy,
                Capacity = eventDto.Capacity,
                RemainingCapacity = eventDto.Capacity,
                Tags = eventDto.Tags
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return newEvent;
        }



        public async Task<EventDetailDto> GetEventByIdAsync(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return null;
            }

            return new EventDetailDto
            {
                Id = eventItem.Id,
                Name = eventItem.Name,
                Description = eventItem.Description,
                Date = eventItem.Date,
                Location = eventItem.Location,
                CreatedBy = eventItem.CreatedBy,
                Capacity = eventItem.Capacity,
                RemainingCapacity = eventItem.RemainingCapacity,
                Tags = eventItem.Tags
            };

        }


        public async Task<IEnumerable<EventDetailDto>> GetAllEventsAsync()
        {
            return await _context.Events
                                 .Include(e => e.Attendees)
                                 .Select(e => new EventDetailDto
                                 {
                                     Id = e.Id,
                                     Name = e.Name,
                                     Description = e.Description,
                                     Date = e.Date,
                                     Location = e.Location,
                                     CreatedBy = e.CreatedBy, 
                                     Capacity = e.Capacity,  
                                     RemainingCapacity = e.RemainingCapacity, 
                                     Tags = e.Tags ,

                                     Attendees = e.Attendees.Select(a=>new AttendeeDto
                                     {   Id = a.Id,
                                         Name = a.Name,
                                         Email = a.Email,
                                     }).ToList()
                                 })
                                 .ToListAsync();
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


        public async Task<String> UpdateEventDetailsAsync(int id, EventUpdateDto eventUpdateDto)
        {
            var existingEvent = await _context.Events
                .FirstOrDefaultAsync(e => e.Id == id);

            if (existingEvent == null)
            {
                return "Event not found!";
            }

            // Update event details
            existingEvent.Name = eventUpdateDto.Name;
            if (eventUpdateDto.Name != null)
                existingEvent.Name = eventUpdateDto.Name;

            if (eventUpdateDto.Description != null)
                existingEvent.Description = eventUpdateDto.Description;

            if (eventUpdateDto.Date != null)
                existingEvent.Date = eventUpdateDto.Date.Value;

            if (eventUpdateDto.Location != null)
                existingEvent.Location = eventUpdateDto.Location;

            if (eventUpdateDto.CreatedBy != null)
                existingEvent.CreatedBy = eventUpdateDto.CreatedBy;

            if (eventUpdateDto.Capacity != null)
                existingEvent.Capacity = eventUpdateDto.Capacity.Value;

            if (eventUpdateDto.RemainingCapacity != null)
                existingEvent.RemainingCapacity = eventUpdateDto.RemainingCapacity.Value;

            if (eventUpdateDto.Tags != null)
                existingEvent.Tags = eventUpdateDto.Tags;

            // Save changes
            await _context.SaveChangesAsync();

            return "Event updated successfully!";
        }









    }
}
