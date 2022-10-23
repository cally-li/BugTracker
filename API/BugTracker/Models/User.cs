namespace BugTracker.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }    
        public string LastName { get; set; }
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string? Role { get; set; }    
         
        public DateTime AccountCreated{ get; set; } 

        public DateTime LastActive { get; set; }    

        public Photo? Photo { get; set; }
        
        public ICollection<Ticket>? AssignedTickets { get; set; }  
        public ICollection<Ticket>? SubmittedTickets { get; set; }

        //project-user: many-to-many
        public ICollection<ProjectUser>? ProjectUsers { get; set; }





    }
}
