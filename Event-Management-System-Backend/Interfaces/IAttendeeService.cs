using Event_Management_System_Backend.Dtos.Attendee;

namespace Event_Management_System_Backend.Interfaces
{
    public interface IAttendeeService
    {
        Task <string>UpdateAttendeeAsync(int eventId , int attendeeId,  UpdateAttendeeDto updateAttendee);
    }
}
