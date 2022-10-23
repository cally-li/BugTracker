namespace BugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }

        public User Commenter { get; set; }

        public DateTime Created { get; set; }
    }
}
