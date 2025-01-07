namespace Event_Management_System_Backend.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Location { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public int RemainingCapacity { get; set; }
        public string Tags { get; set; } = string.Empty;

        public List<Attendee> Attendees { get; set; } = new List<Attendee>();
    }
}
