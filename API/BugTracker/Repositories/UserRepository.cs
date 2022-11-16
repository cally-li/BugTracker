using BugTracker.Data;
using BugTracker.Interfaces;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        //constructor - interact with DbContext
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        //get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync() 
        {
            return await _context.Users.ToListAsync();
        }

        //get user by id
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id); 
        }

        //get user by email
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u=>u.Email==email);
        }

        //get users by project
        public async Task<IEnumerable<User>> GetUsersByProjectAsync(int projectId)
        {
            var projectUser = await _context.ProjectUsers.Where(pu => pu.ProjectId== projectId).ToListAsync();


            //get user ids from ProjectUsers
            var userIDs = new List<int>();
            foreach (var pu in projectUser)
            {
                userIDs.Add(pu.UserId);
            }

            //get all projects by Ids in the list
            return await _context.Users.Where(u => userIDs.Contains(u.Id)).ToListAsync();
        }


        //Ef places a flag on the entity to mark that it has been modified
        //not changing anything in db
        public void Update(User user)
        { 
            _context.Entry(user).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync()>0;
        }

    }
}
