namespace BugTracker.DTOs
{
    //properties that are returned when user logs in
    public class UserDto
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
