namespace Event_Management_System_Backend.Data
{
    using Event_Management_System_Backend.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDBContext : DbContext
    {
       
             public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        // DbSet properties for Event and Attendee models
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        // Optional: Configure entity relationships and constraints using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Example: Configure a many-to-many relationship between Events and Attendees
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Attendees)
                .WithMany(a => a.RegisteredEvents)
                .UsingEntity(j => j.ToTable("EventAttendees")); // Join table

            // Additional configurations (if any) can be added here
        }
        
    }
}
