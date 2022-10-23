namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }

        //project-user: many-to-many
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<Ticket> Tickets { get; set; } 
    }
}