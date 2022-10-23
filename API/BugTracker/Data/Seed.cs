using BugTracker.Models;
using System.Security.Cryptography;
using System.Text;

namespace BugTracker.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }
        public void SeedData()
        {

            if (!_context.Users.Any()) //if no users found in database
            {

                var tickets = new List<Ticket>()
                {
                    new Ticket()
                    {
                        Title = "Ticket1",
                        Description = "Ticket1 description",
                        Status = "Open",
                        Priority= "High",

                    },
                    new Ticket()
                    {
                        Title = "Ticket2",
                        Description = "Ticket2 description",
                        Status = "Open",
                        Priority= "Medium",
                    },
                    new Ticket()
                    {
                        Title = "Ticket3",
                        Description = "Ticket3 description",
                        Status = "Closed",
                        Priority= "Low",

                    }
                };


                var projects = new List<Project>()
                {
                     new Project(){
                        Name = "Project1",
                        Description="BugTracker",
                        Tickets=new List<Ticket>()
                        {
                            tickets[0]
                        }

                    },
                    new Project(){
                        Name = "Project2",
                        Description="Finance Software",
                        Tickets=new List<Ticket>()
                        {
                            tickets[1]
                        },
                    },
                    new Project(){
                        Name = "Project3",
                        Description="Portfolio Page",
                        Tickets=new List<Ticket>()
                        {
                            tickets[2]
                        },
                    }
                };


                var users = new List<User>()
                {
                    new User()
                    {
                        FirstName = "Cally",
                        LastName = "Li",
                        Email = "cally.li@queensu.ca",
                        Role = "Manager",
                        AccountCreated = new DateTime(2020, 1,1),
                        LastActive=new DateTime(2022, 10, 21),
                        Photo = new Photo() { Url = "https://randomuser.me/api/portraits/women/1.jpg"},
                        SubmittedTickets=new List<Ticket>()
                        {
                            tickets[0],
                            tickets[1],
                            tickets[2]
                        },
                        ProjectUsers= new List<ProjectUser>()
                        {
                            new ProjectUser() { Project = projects[0] },
                            new ProjectUser() { Project = projects[1] },
                            new ProjectUser() { Project = projects[2] }
                        }
                          
                    },
                    new User()
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Email = "jane.doe@test.ca",
                        Role = "Developer",
                        AccountCreated = new DateTime(2020, 1,1),
                        LastActive=new DateTime(2022, 10, 15),
                        Photo = new Photo() { Url = "https://randomuser.me/api/portraits/women/2.jpg"},
                        AssignedTickets=new List<Ticket>()
                        {
                            tickets[0],
                            tickets[1],
                        },
                        ProjectUsers= new List<ProjectUser>()
                        {
                            new ProjectUser() { Project = projects[1] },
                        }

                    },
                    new User()
                    {
                        FirstName = "May",
                        LastName = "Parker",
                        Email = "may.parker@test.ca",
                        Role = "Developer",
                        AccountCreated = new DateTime(2020, 1,1),
                        LastActive=new DateTime(2022, 10, 2),
                        Photo = new Photo() { Url = "https://randomuser.me/api/portraits/women/3.jpg"},
                        AssignedTickets=new List<Ticket>()
                        {
                            tickets[2],
                        },
                        ProjectUsers= new List<ProjectUser>()
                        {
                            new ProjectUser() { Project = projects[2] },
                        }
                    }
                };

                //create password for every user
                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                    user.PasswordSalt = hmac.Key;
                }


                _context.Users.AddRange(users);
                _context.SaveChanges();
            }

        }
    }
}