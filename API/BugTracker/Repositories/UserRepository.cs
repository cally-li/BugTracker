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
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        //get user by id
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id); 
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.AssignedTickets).Include(u => u.SubmittedTickets).SingleOrDefaultAsync(u=>u.Email==email);
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
