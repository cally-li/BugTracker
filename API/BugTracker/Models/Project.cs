namespace BugTracker.Models
{
    public class Project
    {
        public int ProjectId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }

        //project-user: many-to-many
        public ICollection<User> AssignedPersonnel { get; set; }
        public ICollection<Ticket> Tickets { get; set; } 
    }
}