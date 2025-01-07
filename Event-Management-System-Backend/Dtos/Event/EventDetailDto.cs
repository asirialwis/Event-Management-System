namespace Event_Management_System_Backend.Dtos.Event
{
    public class EventDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string CreatedBy { get; set; }
        public int Capacity { get; set; }
        public int RemainingCapacity { get; set; }
        public string Tags { get; set; }
    }
}
