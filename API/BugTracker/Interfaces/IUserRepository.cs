using BugTracker.Models;

namespace BugTracker.Interfaces
{
    public interface IUserRepository
    {
        //get all users
        Task<IEnumerable<User>> GetAllUsersAsync();

        //get user by id
        Task<User> GetUserByIdAsync(int id);
        
        //get user by email
        Task<User> GetUserByEmailAsync(string email);

        //get users by project
        Task<IEnumerable<User>> GetUsersByProjectAsync(int projectId);

        //update user
        void Update(User user);

        Task<bool> SaveAllAsync();

    }
}
