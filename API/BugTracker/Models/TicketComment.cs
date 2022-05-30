namespace BugTracker.Models
{
    public class TicketComment
    {
        public int TicketCommentId { get; set; }

        public User Commenter { get; set; }

        public DateTime Created { get; set; }   = DateTime.Now;
    }
}
