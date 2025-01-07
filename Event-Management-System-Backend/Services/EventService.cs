using Event_Management_System_Backend.Data;
using Event_Management_System_Backend.Dtos.Event;
using Event_Management_System_Backend.Interfaces;
using Event_Management_System_Backend.Models;

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
    }
}
