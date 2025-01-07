using Event_Management_System_Backend.Dtos.Event;
using Event_Management_System_Backend.Models;

namespace Event_Management_System_Backend.Interfaces
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(EventDto eventDto);
        Task<EventDetailDto> GetEventByIdAsync(int id);
    }
}
