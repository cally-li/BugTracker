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
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //get user by id
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u=>u.UserId == id); 
        }

    }
}
