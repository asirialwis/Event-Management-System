using Event_Management_System_Backend.Dtos.Attendee;

namespace Event_Management_System_Backend.Interfaces
{
    public interface IAttendeeService
    {

        Task<string> AddAttendeeAsync(int eventId, AddAttendeeDto addAttendeeDto);

        Task <string>UpdateAttendeeAsync(int eventId , int attendeeId,  UpdateAttendeeDto updateAttendee);

        Task<string> DeleteAttendeeAsync(int eventId, int attendeeId);
    }
}
