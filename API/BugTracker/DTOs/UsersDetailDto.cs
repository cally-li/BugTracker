using BugTracker.Models;

namespace BugTracker.DTOs
{
    public class UsersDetailDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Role { get; set; }

   
    }
}
