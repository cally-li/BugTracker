using BugTracker.Models;

namespace BugTracker.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }


        public UsersDetailDto AssignedDeveloper { get; set; }

        public UsersDetailDto Submitter { get; set; }

        public int ProjectId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }


    }
}
