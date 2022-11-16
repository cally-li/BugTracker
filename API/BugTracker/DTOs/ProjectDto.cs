
using BugTracker.Models;

namespace BugTracker.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<TicketDto> Tickets { get; set; }


    }
}
