using BugTracker.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task SeedData()
        {
            await _context.Database.MigrateAsync(); 

            if (!await _context.Users.AnyAsync())
            {
                var project = new Project()
                {
                    Name = "Project1",
                    Description = "bugtracker",
                    Tickets = new List<Ticket>()
                        {
                            new Ticket()
                            {
                                Title = "Ticket1",
                                AssignedDeveloperId = 2,
                                SubmitterId = 1,
                                Description = "Ticket1 description",
                                Status = "Open",
                                Priority= "High",
                                Type = "Bug",

                            },
                            new Ticket()
                            {
                                Title = "Ticket2",
                                AssignedDeveloperId = 2,
                                SubmitterId = 1,
                                Description = "Ticket2 description",
                                Status = "Open",
                                Priority= "Medium",
                                Type = "Bug"
                            }
                        }
                };
                _context.Projects.Add(project);


                var users = new List<User>()
                {
                    new User()
                    {

                        FirstName = "Cally",
                        LastName = "Li",
                        Email = "cally.li@queensu.ca",
                        Role = "Admin"
                    },
                    new User()
                    {
                        FirstName = "Jane",
                        LastName = "Doe",
                        Email = "jane.doe@test.ca",
                        Role = "Developer"
                    },
                    new User()
                    {
                        FirstName = "Edward",
                        LastName = "Scissorhands",
                        Email = "edward.scissorhands@test.ca",
                        Role = "Developer"

                    },
                    new User()
                    {
                        FirstName = "Ben",
                        LastName = "Parker",
                        Email = "ben.parker@test.ca",
                        Role = "Developer"

                    }

                };

                foreach (var user in users)
                {
                    using var hmac = new HMACSHA512();
                    user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                    user.PasswordSalt = hmac.Key;
                    _context.Users.Add(user);
                }


                await _context.SaveChangesAsync();
            }

        }
    }
}
