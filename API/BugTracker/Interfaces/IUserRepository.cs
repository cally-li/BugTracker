using BugTracker.Models;

namespace BugTracker.Interfaces
{
    public interface IUserRepository
    {
        //get all users
        Task<List<User>> GetAllUsersAsync();

        //get user by id
        Task<User> GetUserByIdAsync(int id);
        
        //get user by email
        Task<User> GetUserByEmailAsync(string email);

        void Update(User user);

        Task<bool> SaveAllAsync();

    }
}
