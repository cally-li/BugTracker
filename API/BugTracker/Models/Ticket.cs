namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public DateTime Created { get; set; }

        public int AssignedDeveloperId { get; set; }
        public User AssignedDeveloper { get; set; }
        public int SubmitterId { get; set; }

        public User Submitter { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }

        
        public ICollection<TicketComment> Comments { get; set; }

        public ICollection<TicketHistoryItem> History { get; set; }



    }
}