using BugTracker.Models;

namespace BugTracker.DTOs
{
    public class UserDetailDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }
        public ICollection<TicketDto>? AssignedTickets { get; set; }
        public ICollection<TicketDto>? SubmittedTickets { get; set; }
    }
}
