namespace Event_Management_System_Backend.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<Event> RegisteredEvents { get; set; } = new List<Event>();
    }
}
