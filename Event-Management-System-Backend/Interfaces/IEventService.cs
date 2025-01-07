using Event_Management_System_Backend.Dtos.Attendee;
using Event_Management_System_Backend.Dtos.Event;
using Event_Management_System_Backend.Models;

namespace Event_Management_System_Backend.Interfaces
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(EventDto eventDto);

        Task<IEnumerable<EventDetailDto>> GetAllEventsAsync();

        Task<EventDetailDto> GetEventByIdAsync(int id);

        Task<string> UpdateEventDetailsAsync(int eventId, EventUpdateDto eventUpdateDto);

        Task<string> DeleteEventAsync(int eventId);
    }
}
