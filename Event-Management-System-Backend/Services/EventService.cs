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
                                     Tags = e.Tags 
                                 })
                                 .ToListAsync();
        }


        public async Task<string> AddAttendeeAsync(int eventId, AttendeeDto attendeeDto)
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
                Name = attendeeDto.Name,
                Email = attendeeDto.Email
            };

            eventEntity.Attendees.Add(attendee);
            eventEntity.RemainingCapacity -= 1;

            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return "Attendee added successfully.";
        }





    }
}
