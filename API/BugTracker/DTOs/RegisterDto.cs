using System.ComponentModel.DataAnnotations;

namespace BugTracker.DTOs
{
    public class RegisterDto
    {
        //mark required fields
        [Required(ErrorMessage ="First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(30, ErrorMessage = "{0} must be between {2}-{1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

    }
}
