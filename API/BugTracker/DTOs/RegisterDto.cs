using System.ComponentModel.DataAnnotations;

namespace BugTracker.DTOs
{
    public class RegisterDto
    {
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(15,  MinimumLength = 6)]
        public string Password { get; set; }

    }
}
